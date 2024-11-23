using APIWeb1.Data;
using APIWeb1.Dtos.Statisticals;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Repository
{
    public class StatisticalRepository : IStatisticalRepository
    {
        private readonly ApplicationDBContext _context;
        public StatisticalRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<StatisticalGetStatusJob> GetStatisticalJobStatus(string employerId)
        {
            var statusApproved = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var statusPending = EnumHelper.GetEnumValueFromDescription<JobStatus>("Pending");
            DateTime dateTime = DateTime.Now;

            int jobApproved =  await _context.Jobs.Where(u=> u.JobStatus== statusApproved && u.EmployerId== employerId).CountAsync();
            int jobPending = await _context.Jobs.Where(u => u.JobStatus == statusPending && u.EmployerId == employerId).CountAsync();
            
            int jobExpire = await _context.Jobs.Where(u => u.ExpiredDate < dateTime && u.EmployerId == employerId).CountAsync();

            StatisticalGetStatusJob getJobStatus = new StatisticalGetStatusJob();
            getJobStatus.Approval = jobApproved;
            getJobStatus.Pending = jobPending;
            getJobStatus.Expired = jobExpire;
            return getJobStatus;
        }

        public async Task<StatisticalGetTotalJobAndAppli> GetStatisticalTotalJobAndAppli(string employerId)
        {

            int totalJob = await _context.Jobs.Where(u=> u.EmployerId == employerId).CountAsync();
            int totalApply = await _context.Applications.Include(u=>u.Job).Where(a => a.Job.EmployerId == employerId && a.Status!=0).CountAsync();

            StatisticalGetTotalJobAndAppli gettotal = new StatisticalGetTotalJobAndAppli()
            {
                totalJob = totalJob,
                totalApply = totalApply
            };
            
            return gettotal;
        }
    }
}
