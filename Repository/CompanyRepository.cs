using APIWeb1.Data;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDBContext _context;
        public CompanyRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.Companys.Where(u=> u.Status==true).ToListAsync();
        }

        public async Task<List<Company>> GetAllAsyncForUser(CompanyQueryObj query)
        {
            var company = _context.Companys.Where(s=> s.Status==true).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                company = company.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Industry))
            {
                company = company.Where(s => s.Industry.Contains(query.Industry));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    company = query.IsDecsending ? company.OrderByDescending(s => s.Name) : company.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await company.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public void Update(Company company)
        {
            _context.Companys.Update(company);
        }
    }
}
