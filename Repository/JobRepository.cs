using APIWeb1.Data;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDBContext _context;
        public JobRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Job> CreateAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<List<JobDto>> GetAllAsync(JobQueryObject query)
        {
            var job =  _context.Jobs.Include(a => a.Employer).ThenInclude(b => b.Company).Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                job = job.Where(s => s.Location.Contains(query.Location));
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
                ExpiredDate = job.ExpiredDate,
                CreateOn = job.CreateOn,
                UpdatedOn = job.UpdatedOn,
                Employer = job.Employer,
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                Location=job.Location,
                Skills = job.JobSkills.Select(js => new Skill
                {


                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync(); 
        }

        public async Task<List<JobDto>> GetUserJob(AppUser user, JobQueryObject query)
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
                job = job.Where(s => s.Location.Contains(query.Location));
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
                ExpiredDate = job.ExpiredDate,
                CreateOn = job.CreateOn,
                UpdatedOn = job.UpdatedOn,
                Employer = null,
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                Location = job.Location,
                Skills = job.JobSkills.Select(js => new Skill
                {


                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync();
        }
    }
}
