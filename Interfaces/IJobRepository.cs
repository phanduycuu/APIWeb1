using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IJobRepository
    {
        Task<List<JobDto>> GetEmployerJob(AppUser user, JobQueryObject query);
        Task<Job> CreateAsync(Job job);
        Task<List<JobDto>> GetAllAsync(JobQueryObject query);
    }
}
