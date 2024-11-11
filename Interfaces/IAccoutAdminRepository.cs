using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IAccoutAdminRepository
    {
        Task<List<AppUser>> GetAllAsync(string role);
        Task<bool> UpdateAsync(string userId,int status);
    }
}
