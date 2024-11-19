using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetAllAsync();
        Task<List<Company>> GetAllAsyncForUser(CompanyQueryObj query);
        //Task<Company?> GetCompanyById(int companyId);
        void Update(Company company);
    }
}
