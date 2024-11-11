using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetAllAsync();
        void Update(Company company);
    }
}
