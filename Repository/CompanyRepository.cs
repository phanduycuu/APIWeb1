using APIWeb1.Data;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Companys;
using APIWeb1.Dtos.Job;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
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
            if (query.PageSize == 0 && query.PageNumber == 0)
                return await company.ToListAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await company.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public void Update(Company company)
        {
            _context.Companys.Update(company);
        }

        public async Task<GetCompanybyIdDto> GetCompanyWithJobsByIdAsync(int companyId)
        {
            var company = await _context.Companys
                .Where(c => c.Id == companyId)
                .Select(c => new GetCompanybyIdDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Industry = c.Industry,
                    Description = c.Description,
                    Logo = c.Logo,
                    Website = c.Website,
                    Email = c.Email,
                    Phone = c.Phone,
                    Create = c.Create,
                    Update = c.Update,
                    Status = c.Status,
                    Jobs = c.Employers
                        .SelectMany(u => u.Jobs)
                        .Select(job => new GetAllJobDto
                        {
                            Id = job.Id,
                            Title = job.Title,
                            Salary = job.Salary,
                            CreateOn = job.CreateOn,

                            Employer = new GetEmployer
                            {
                                Id = job.Employer.Id,
                                Company = new GetCompany
                                {
                                    Name = job.Employer.Company.Name,
                                    logo = job.Employer.Company.Logo
                                },
                            },
                            JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                            JobType = EnumHelper.GetEnumDescription(job.JobType),
                            JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                            LocationShort = job.Address.Province + ", " + job.Address.District,
                            Skills = job.JobSkills.Select(js => new SkillDto
                            {
                                Id = js.Skill.Id,
                                Name = js.Skill.Name
                                // Include other properties of Skill as needed

                            }).ToList()

                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return company;
        }

        public async Task<int> GetTotalAsync()
        {
            var totalcompanys = await _context.Companys
                                  .Where(u => u.Status == true)
                                  .CountAsync();

            return totalcompanys;
        }

        public Task<int> GetTotalWithConditionsAsync(CompanyQueryObj query)
        {
            var companyQuery = _context.Companys.Where(s => s.Status == true);


            // Áp dụng các bộ lọc
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                companyQuery = companyQuery.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Industry))
            {
                companyQuery = companyQuery.Where(s => s.Industry.Contains(query.Industry));
            }

            // Tính tổng số công ty thoả mãn các điều kiện
            var totalCompanys = companyQuery.CountAsync();

            return totalCompanys;
        }
    }
}
