using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
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

        public async Task<List<Job>> GetAllAsync()
        {
            return await _context.Jobs.Include(a=>a.Employer).ThenInclude(b=>b.Company).Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill)
            .Select(job => new Job
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
                JobLevel = job.JobLevel,
                JobStatus = job.JobStatus,
                JobType = job.JobType,
                Skills = job.JobSkills.Select(js => new Skill
                {


                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync(); 
        }

        public async Task<List<Job>> GetUserJob(AppUser user)
        {
            return await _context.Jobs
        .Where(job => job.EmployerId == user.Id)
        .Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill)
        .Select(job => new Job
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Requirements = job.Requirements,
            Benefits = job.Benefits,
            ExpiredDate = job.ExpiredDate,
            CreateOn = job.CreateOn,
            UpdatedOn = job.UpdatedOn,
            JobLevel = job.JobLevel,
            JobStatus = job.JobStatus,
            JobType = job.JobType,
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
