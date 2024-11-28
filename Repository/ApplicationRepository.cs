﻿using APIWeb1.Data;
using APIWeb1.Dtos.Application;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Job;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APIWeb1.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDBContext _context;
        public ApplicationRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Application> CreateAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<Application> GetAppUserJob(int JobId, string userId)
        {
           
            return await _context.Applications.Where(a=>a.JobId==JobId && a.UserId==userId).FirstOrDefaultAsync();
        }

        public async Task<GetIsaveAndStatus> GetIssvaAndStatus(int JobId, string userId)
        {

            var app= await _context.Applications.Where(a => a.JobId == JobId && a.UserId == userId).FirstOrDefaultAsync();
            if (app == null)
            {
                return null;
            }
            GetIsaveAndStatus dto= new GetIsaveAndStatus();
            dto.Status = app.Status;
            dto.Issave = app.Issave;
            return dto;
        }

        public async Task<Application> GetEmployerApp(int JobId, string userId, string employerId)
        {
            return await _context.Applications.Include(u=> u.Job).ThenInclude(j=>j.Employer).Where(a => a.JobId == JobId && a.UserId == userId && a.Job.EmployerId== employerId).FirstOrDefaultAsync();
        }

        public async Task<List<GetAppDto>> GetUserJob(AppUser user, JobQueryObject query)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var applications = _context.Applications.Include(a => a.Job).
                ThenInclude(a => a.Employer).ThenInclude(b => b.Company).Include(a => a.Job).ThenInclude(a => a.Address)
                .Include(a => a.Job).ThenInclude(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u => u.UserId == user.Id && u.Status!=0 && u.Job.IsShow == true && u.Job.ExpiredDate > DateTime.Now && u.Job.JobStatus == status);
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                applications = applications.Where(s => s.Job.Title.Contains(query.Title));
            }

            

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreateOn", StringComparison.OrdinalIgnoreCase))
                {
                    applications = query.IsDecsending ? applications.OrderByDescending(s => s.Job.CreateOn) : applications.OrderBy(s => s.Job.CreateOn);
                }
            }

            if (query.PageSize == 0 && query.PageNumber == 0)
            return await applications.Select(app => new GetAppDto
            {
                Id = app.Job.Id,
                Title = app.Job.Title,
                Description = app.Job.Description,
                Requirements = app.Job.Requirements,
                Benefits = app.Job.Benefits,
                Salary = app.Job.Salary,
                ExpiredDate = app.Job.ExpiredDate,
                CreateOn = app.Job.CreateOn,
                UpdatedOn = app.Job.UpdatedOn,
                Employer = new EmployerDto{
                    Id= app.Job.Employer.Id,
                    FullName= app.Job.Employer.Fullname,
                    Email = app.Job.Employer.Email,
                    Company= app.Job.Employer.Company
                },
                JobLevel = EnumHelper.GetEnumDescription(app.Job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(app.Job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(app.Job.JobStatus),
                Location = app.Job.Address.Street+" "+app.Job.Address.Province+" "
                          +app.Job.Address.Ward + " " + app.Job.Address.District,
                CV = app.Cv,
                Skills = app.Job.JobSkills.Select(js => new SkillDto
                {
                    Name = js.Skill.Name

                }).ToList()
            })
            .ToListAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await applications.Skip(skipNumber).Take(query.PageSize)
                .Select(app => new GetAppDto
                {
                    Id = app.Job.Id,
                    Title = app.Job.Title,
                    Description = app.Job.Description,
                    Requirements = app.Job.Requirements,
                    Benefits = app.Job.Benefits,
                    Salary = app.Job.Salary,
                    ExpiredDate = app.Job.ExpiredDate,
                    CreateOn = app.Job.CreateOn,
                    UpdatedOn = app.Job.UpdatedOn,
                    Employer = new EmployerDto
                    {
                        Id = app.Job.Employer.Id,
                        FullName = app.Job.Employer.Fullname,
                        Email = app.Job.Employer.Email,
                        Company = app.Job.Employer.Company
                    },
                    JobLevel = EnumHelper.GetEnumDescription(app.Job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(app.Job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(app.Job.JobStatus),
                    Location = app.Job.Address.Street + " " + app.Job.Address.Province + " "
                          + app.Job.Address.Ward + " " + app.Job.Address.District,
                    CV = app.Cv,
                    Skills = app.Job.JobSkills.Select(js => new SkillDto
                    {
                        Name = js.Skill.Name

                    }).ToList()
                })
            .ToListAsync();

        }

        public async Task<List<GetAppDto>> GetUserJobSearchByTitle(AppUser user, JobQueryObject query)
        {
            var applications = _context.Applications.Include(a => a.Job).
                ThenInclude(a => a.Employer).ThenInclude(b => b.Company).Include(a => a.Job).ThenInclude(a => a.Address)
                .Include(a => a.Job).ThenInclude(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u => u.UserId == user.Id && u.Status != 0);
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                applications = applications.Where(s => s.Job.Title.Contains(query.Title));
            }

            if (query.PageSize == 0 && query.PageNumber == 0)
                return await applications.Select(app => new GetAppDto
                {
                    Id = app.Job.Id,
                    Title = app.Job.Title,
                    Description = app.Job.Description,
                    Requirements = app.Job.Requirements,
                    Benefits = app.Job.Benefits,
                    Salary = app.Job.Salary,
                    ExpiredDate = app.Job.ExpiredDate,
                    CreateOn = app.Job.CreateOn,
                    UpdatedOn = app.Job.UpdatedOn,
                    Employer = new EmployerDto
                    {
                        Id = app.Job.Employer.Id,
                        FullName = app.Job.Employer.Fullname,
                        Email = app.Job.Employer.Email,
                        Company = app.Job.Employer.Company
                    },
                    JobLevel = EnumHelper.GetEnumDescription(app.Job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(app.Job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(app.Job.JobStatus),
                    Location = app.Job.Address.Street + " " + app.Job.Address.Province + " "
                              + app.Job.Address.Ward + " " + app.Job.Address.District,
                    CV = app.Cv,
                    Skills = app.Job.JobSkills.Select(js => new SkillDto
                    {
                        Name = js.Skill.Name

                    }).ToList()
                })
                .ToListAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await applications.Skip(skipNumber).Take(query.PageSize)
                .Select(app => new GetAppDto
                {
                    Id = app.Job.Id,
                    Title = app.Job.Title,
                    Description = app.Job.Description,
                    Requirements = app.Job.Requirements,
                    Benefits = app.Job.Benefits,
                    Salary = app.Job.Salary,
                    ExpiredDate = app.Job.ExpiredDate,
                    CreateOn = app.Job.CreateOn,
                    UpdatedOn = app.Job.UpdatedOn,
                    Employer = new EmployerDto
                    {
                        Id = app.Job.Employer.Id,
                        FullName = app.Job.Employer.Fullname,
                        Email = app.Job.Employer.Email,
                        Company = app.Job.Employer.Company
                    },
                    JobLevel = EnumHelper.GetEnumDescription(app.Job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(app.Job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(app.Job.JobStatus),
                    Location = app.Job.Address.Street + " " + app.Job.Address.Province + " "
                          + app.Job.Address.Ward + " " + app.Job.Address.District,
                    CV = app.Cv,
                    Skills = app.Job.JobSkills.Select(js => new SkillDto
                    {
                        Name = js.Skill.Name

                    }).ToList()
                })
            .ToListAsync();
        }


        public async Task<List<GetAppDto>> GetAppUserIsSaveJob(AppUser user, JobQueryObject query)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var applications = _context.Applications.Include(a => a.Job).
                ThenInclude(a => a.Employer).ThenInclude(b => b.Company).Include(a => a.Job).ThenInclude(a => a.Address)
                .Include(a => a.Job).ThenInclude(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u => u.UserId == user.Id && u.Issave == true && u.Job.IsShow == true && u.Job.ExpiredDate > DateTime.Now && u.Job.JobStatus == status);
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                applications = applications.Where(s => s.Job.Title.Contains(query.Title));
            }
            if (query.PageSize == 0 && query.PageNumber == 0)
                return await applications.Select(app => new GetAppDto
                {
                    Id = app.Job.Id,
                    Title = app.Job.Title,
                    Description = app.Job.Description,
                    Requirements = app.Job.Requirements,
                    Benefits = app.Job.Benefits,
                    Salary = app.Job.Salary,
                    ExpiredDate = app.Job.ExpiredDate,
                    CreateOn = app.Job.CreateOn,
                    UpdatedOn = app.Job.UpdatedOn,
                    Employer = new EmployerDto
                    {
                        Id = app.Job.Employer.Id,
                        FullName = app.Job.Employer.Fullname,
                        Email = app.Job.Employer.Email,
                        Company = app.Job.Employer.Company
                    },
                    JobLevel = EnumHelper.GetEnumDescription(app.Job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(app.Job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(app.Job.JobStatus),
                    Location = app.Job.Address.Street + " " + app.Job.Address.Province + " "
                              + app.Job.Address.Ward + " " + app.Job.Address.District,
                    CV = app.Cv,
                    Skills = app.Job.JobSkills.Select(js => new SkillDto
                    {
                        Name = js.Skill.Name

                    }).ToList()
                })
                .ToListAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await applications.Skip(skipNumber).Take(query.PageSize)
                .Select(app => new GetAppDto
                {
                    Id = app.Job.Id,
                    Title = app.Job.Title,
                    Description = app.Job.Description,
                    Requirements = app.Job.Requirements,
                    Benefits = app.Job.Benefits,
                    Salary = app.Job.Salary,
                    ExpiredDate = app.Job.ExpiredDate,
                    CreateOn = app.Job.CreateOn,
                    UpdatedOn = app.Job.UpdatedOn,
                    Employer = new EmployerDto
                    {
                        Id = app.Job.Employer.Id,
                        FullName = app.Job.Employer.Fullname,
                        Email = app.Job.Employer.Email,
                        Company = app.Job.Employer.Company
                    },
                    JobLevel = EnumHelper.GetEnumDescription(app.Job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(app.Job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(app.Job.JobStatus),
                    Location = app.Job.Address.Street + " " + app.Job.Address.Province + " "
                          + app.Job.Address.Ward + " " + app.Job.Address.District,
                    CV = app.Cv,
                    Skills = app.Job.JobSkills.Select(js => new SkillDto
                    {
                        Name = js.Skill.Name

                    }).ToList()
                })
            .ToListAsync();
        }

        public async Task<Application> UpdateAppUserJob(Application app)
        {
            var exitstapp = await _context.Applications.FirstOrDefaultAsync(x => x.Id == app.Id);
            exitstapp = app;
            await _context.SaveChangesAsync();
            return exitstapp;
        }
    }
}
