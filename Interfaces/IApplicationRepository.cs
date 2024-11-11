using APIWeb1.Dtos.Application;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application> CreateAsync(Application application); // khi user muon apply job
        Task<List<GetAppDto>> GetUserJob(AppUser user, JobQueryObject query); // khi user muon lay danh sach cac job da apply
        Task<Application> GetAppUserJob(int JobId,string userId);
        Task<Application> UpdateAppUserJob(Application app);

    }
}
