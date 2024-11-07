using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IJobSkillRepository
    {
        Task<JobSkill> CreateAsync(JobSkill jobskill);
        Task<List<Skill>> GetJobSkill(int jobId);
    }
}
