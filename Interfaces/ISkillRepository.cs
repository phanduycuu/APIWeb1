using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<Skill>> GetAllAsync();
        void Update(Skill skill);
    }
}
