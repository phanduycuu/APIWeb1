using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Repository
{
    public class JobSkillRepository : IJobSkillRepository
    {
        private readonly ApplicationDBContext _context;
        public JobSkillRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<JobSkill> CreateAsync(JobSkill jobskill)
        {
            await _context.JobSkills.AddAsync(jobskill);
            await _context.SaveChangesAsync();
            return jobskill;
        }

        public async Task<List<Skill>> GetJobSkill(int jobId)
        {
            return await _context.JobSkills.Where(u => u.JobId == jobId)
            .Select(skill => new Skill
            {
                Id = skill.Skill.Id,
                Name = skill.Skill.Name
            }).ToListAsync();
        }
    }
}
