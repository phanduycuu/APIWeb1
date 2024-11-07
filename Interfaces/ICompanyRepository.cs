using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync();
    }
}
