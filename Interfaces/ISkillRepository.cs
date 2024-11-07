using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();
    }
}
