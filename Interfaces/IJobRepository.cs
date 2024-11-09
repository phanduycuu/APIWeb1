using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IJobRepository
    {
        Task<List<JobDto>> GetEmployerJob(AppUser user, JobQueryObject query); //khi employer muon lay danh sach cac job da tao
        Task<Job> CreateAsync(Job job); // employer tao job
        Task<List<JobDto>> GetAllAsync(JobQueryObject query);  // hien ra tat ca job
        Task<List<GetJobByIdDto>> GetJobById(int JobId); //khi employer co danh sach cac job da dang, khi an vo chi tiet se lay job theo id
    }
}
