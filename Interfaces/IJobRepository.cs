using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IJobRepository
    {
        Task<List<Job>> GetUserJob(AppUser user);
        Task<Job> CreateAsync(Job job);
        Task<List<Job>> GetAllAsync();
    }
}
