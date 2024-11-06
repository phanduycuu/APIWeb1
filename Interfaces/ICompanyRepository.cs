using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ICompanyRepository: IRepository<Company>
    {
        Task UpdateAsync(Company company);
    }
}
