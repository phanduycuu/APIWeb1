using APIWeb1.Dtos.Companys;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetAllAsync();
        Task<List<Company>> GetAllAsyncForUser(CompanyQueryObj query);
        Task<GetCompanybyIdDto> GetCompanyWithJobsByIdAsync(int companyId);
        Task<int> GetTotalAsync();

        Task<int> GetTotalWithConditionsAsync(CompanyQueryObj query);
        void Update(Company company);
    }
}
