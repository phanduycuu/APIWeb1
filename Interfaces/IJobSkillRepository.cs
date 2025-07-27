using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IJobSkillRepository
    {
        Task<JobSkill> CreateAsync(JobSkill jobskill);
        Task<List<SkillDto>> GetJobSkill(int jobId);
    }
}
