using APIWeb1.Data;
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
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly ApplicationDBContext _context;
        public JobRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Job> AdminGetJobById(int jobId)
        {
            return await _context.Jobs.Where(u=>u.Id== jobId).FirstOrDefaultAsync();
        }

        public async Task<Job> CreateAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<List<JobAdminDto>> GetAdminJob()
        {
            var job = _context.Jobs.Include(a => a.Employer).ThenInclude(b => b.Company).AsQueryable();

            return await job
                .Select(job => new JobAdminDto
                {
                    Id = job.Id,
                    Title = job.Title,
                    EmployerName = job.Employer.Fullname,
                    JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                    
                })
            .ToListAsync();
        }

        public async Task<List<JobDto>> GetAllAsync(JobQueryObject query)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var job =  _context.Jobs.Include(a => a.Employer).ThenInclude(b => b.Company).Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u=> u.JobStatus == status).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                job = job.Where(s => (s.Address.Street + " " +
                          s.Address.Province + " " +
                          s.Address.Ward + " " +
                          s.Address.District)
                          .Contains(query.Location));
            }
            if(!string.IsNullOrWhiteSpace(query.JobLevel))
            {
                var level = EnumHelper.GetEnumValueFromDescription<JobLevel>(query.JobLevel);
                job = job.Where(s => s.JobLevel== level);
            }
            if (!string.IsNullOrWhiteSpace(query.JobStatus))
            {
                var Status = EnumHelper.GetEnumValueFromDescription<JobStatus>(query.JobStatus);
                job = job.Where(s => s.JobStatus == Status);
            }
            if (!string.IsNullOrWhiteSpace(query.JobType))
            {
                var Type = EnumHelper.GetEnumValueFromDescription<JobType>(query.JobType);
                job = job.Where(s => s.JobType == Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    job = query.IsDecsending ? job.OrderByDescending(s => s.Title) : job.OrderBy(s => s.Title);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await job.Skip(skipNumber).Take(query.PageSize)
            .Select(job => new JobDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                ExpiredDate = job.ExpiredDate,
                CreateOn = job.CreateOn,
                UpdatedOn = job.UpdatedOn,
                Employer = new EmployerDto
                {
                    Id = job.Employer.Id,
                    FullName = job.Employer.Fullname,
                    Email = job.Employer.Email,
                    Company = job.Employer.Company
                },
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                Location= job.Address.Street + " " +
                              job.Address.Province + " " +
                              job.Address.Ward + " " +
                              job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id=js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync(); 
        }

        public async Task<List<JobDto>> GetEmployerJob(AppUser user, JobQueryObject query)
        {
            var job =  _context.Jobs
                        .Where(job => job.EmployerId == user.Id).Include(job => job.JobSkills)
                        .ThenInclude(jobSkill => jobSkill.Skill).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                job = job.Where(s => (s.Address.Street + " " +
                          s.Address.Province + " " +
                          s.Address.Ward + " " +
                          s.Address.District)
                          .Contains(query.Location));
            }
            if (!string.IsNullOrWhiteSpace(query.JobLevel))
            {
                var level = EnumHelper.GetEnumValueFromDescription<JobLevel>(query.JobLevel);
                job = job.Where(s => s.JobLevel == level);
            }
            if (!string.IsNullOrWhiteSpace(query.JobStatus))
            {
                var Status = EnumHelper.GetEnumValueFromDescription<JobStatus>(query.JobStatus);
                job = job.Where(s => s.JobStatus == Status);
            }
            if (!string.IsNullOrWhiteSpace(query.JobType))
            {
                var Type = EnumHelper.GetEnumValueFromDescription<JobType>(query.JobType);
                job = job.Where(s => s.JobType == Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    job = query.IsDecsending ? job.OrderByDescending(s => s.Title) : job.OrderBy(s => s.Title);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await job.Skip(skipNumber).Take(query.PageSize)
            .Select(job => new JobDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                ExpiredDate = job.ExpiredDate,
                CreateOn = job.CreateOn,
                UpdatedOn = job.UpdatedOn,
                Employer = null,
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                Location = job.Address.Street + " " +
                              job.Address.Province + " " +
                              job.Address.Ward + " " +
                              job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                }).ToList()
            })
        .ToListAsync();
        }
        public Task<List<GetJobByIdDto>> GetJobById(int JobId)
        {
            var jobModel = _context.Jobs.Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u => u.Id == JobId);

            return jobModel.Select(job => new GetJobByIdDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                ExpiredDate = job.ExpiredDate,
                CreateOn = job.CreateOn,
                UpdatedOn = job.UpdatedOn,
                Users = job.Applications.Where(u=>u.Status !=0).Select(user => new AppUserDto
                {

                    FullName = user.User.Fullname,
                    Email = user.User.Email,
                    CV = user.Cv
                    // Include other properties of Skill as needed

                }).ToList(),
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                Location = job.Address.Street + " " +
                              job.Address.Province + " " +
                              job.Address.Ward + " " +
                              job.Address.District,
                Skills = job.JobSkills.Select(js => new Skill
                {


                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            }).ToListAsync();
        }

        public void UpdateStatusJob(int JobId, JobStatus Status)
        {   
            Job job = _context.Jobs.Where(u=> u.Id== JobId).FirstOrDefault();
            job.JobStatus= Status;
            _context.Jobs.Update(job);
            _context.SaveChanges();

        }
    }

}
