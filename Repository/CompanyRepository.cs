using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private ApplicationDBContext _context;
        public CompanyRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companys.Where(u=> u.Status==true).ToListAsync();
        }
    }
}
