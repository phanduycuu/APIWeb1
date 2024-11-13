using APIWeb1.Data;
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

        public async Task<List<GetAppDto>> GetUserJob(AppUser user, JobQueryObject query)
        {
            var applications = _context.Applications.Include(a => a.Job).
                ThenInclude(a => a.Employer).ThenInclude(b => b.Company).Include(a => a.Job).ThenInclude(a => a.Address)
                .Include(a => a.Job).ThenInclude(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u => u.UserId == user.Id);
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                applications = applications.Where(s => s.Job.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                
                applications = applications.Where(s => (s.Job.Address.Street+" " +
                s.Job.Address.Province + " " +
                s.Job.Address.Ward + " " +
                s.Job.Address.District ).Contains(query.Location));
            }
            if (!string.IsNullOrWhiteSpace(query.JobLevel))
            {
                var level = EnumHelper.GetEnumValueFromDescription<JobLevel>(query.JobLevel);
                applications = applications.Where(s => s.Job.JobLevel == level);
            }
            if (!string.IsNullOrWhiteSpace(query.JobStatus))
            {
                var Status = EnumHelper.GetEnumValueFromDescription<JobStatus>(query.JobStatus);
                applications = applications.Where(s => s.Job.JobStatus == Status);
            }
            if (!string.IsNullOrWhiteSpace(query.JobType))
            {
                var Type = EnumHelper.GetEnumValueFromDescription<JobType>(query.JobType);
                applications = applications.Where(s => s.Job.JobType == Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    applications = query.IsDecsending ? applications.OrderByDescending(s => s.Job.Title) : applications.OrderBy(s => s.Job.Title);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await applications.Skip(skipNumber).Take(query.PageSize).Select(app => new GetAppDto
            {
                Id = app.Job.Id,
                Title = app.Job.Title,
                Description = app.Job.Description,
                Requirements = app.Job.Requirements,
                Benefits = app.Job.Benefits,
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
