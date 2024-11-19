using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;
using APIWeb1.Models.Enum;

namespace APIWeb1.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<List<JobDto>> GetEmployerJob(AppUser user, JobQueryObject query); //khi employer muon lay danh sach cac job da tao
        Task<Job> CreateAsync(Job job); // employer tao job
        Task<List<JobDto>> GetAllAsync(JobQueryObject query);  // hien ra tat ca job
        Task<List<GetJobByIdDto>> GetJobById(int JobId); //khi employer co danh sach cac job da dang, khi an vo chi tiet se lay job theo id
        Task<List<JobAdminDto>> GetAdminJob(); // admin lấy danh sách job để duyệt
        void UpdateStatusJob(int JobId,JobStatus Status);// admin lấy  job thông qua id sau đó update lại trạng thái
        Task<int> GetTotalAsync();
        Task<Job> AdminGetJobById(int jobId);
    }
}
