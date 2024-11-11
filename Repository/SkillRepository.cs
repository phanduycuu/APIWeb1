using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Repository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly ApplicationDBContext _context;
        public SkillRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Skill>> GetAllAsync()
        {
            return await _context.Skills.Where(u=> u.IsDeleted==false).ToListAsync();
        }
        public void Update(Skill skill)
        {
            _context.Skills.Update(skill);
        }
    }
}
