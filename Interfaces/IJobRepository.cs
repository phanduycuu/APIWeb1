﻿using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;
using APIWeb1.Models.Enum;

namespace APIWeb1.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<List<GetAllJobDto>> GetEmployerJob(AppUser user, JobQueryObject query); //khi employer muon lay danh sach cac job da tao
        Task<Job> CreateAsync(Job job); // employer tao job
        Task<List<GetAllJobDto>> GetAllAsync(JobQueryObject query);  // hien ra tat ca job
        Task<GetJobByIdDto> GetJobById(int JobId,string EmployerId); //khi employer co danh sach cac job da dang, khi an vo chi tiet se lay job theo id
        Task<List<JobAdminDto>> GetAdminJob(); // admin lấy danh sách job để duyệt
        void UpdateStatusJob(int JobId,JobStatus Status);// admin lấy  job thông qua id sau đó update lại trạng thái
        Task<int> GetTotalAsync();
        Task<int> GetTotalForEmployer(AppUser user, JobQueryObject query);
        //Task<int> GetTotalforEmployerAsync(AppUser user);
        Task<int> GetTotalWithConditionsAsync(JobQueryObject query);

        Task<JobDto> GetJobByIdForAll(int JobId);
        Task<Job> AdminGetJobById(int jobId);
        Task<Job> UpdateEmployerJob(Job job);
    }
}
