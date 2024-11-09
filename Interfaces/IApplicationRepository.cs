using APIWeb1.Dtos.Application;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application> CreateAsync(Application application);
        Task<List<GetAppDto>> GetUserJob(AppUser user, JobQueryObject query);

        Task<List<GetAppDto>> GetJobById(int JobId);
    }
}
