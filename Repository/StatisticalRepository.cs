using APIWeb1.Data;
using APIWeb1.Dtos.Statisticals;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIWeb1.Repository
{
    public class StatisticalRepository : IStatisticalRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _context;
        public StatisticalRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<StatisticalGetStatusJob> GetStatisticalJobStatus(string employerId)
        {
            var statusApproved = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var statusPending = EnumHelper.GetEnumValueFromDescription<JobStatus>("Pending");
            var statusRejected = EnumHelper.GetEnumValueFromDescription<JobStatus>("Rejected");
            DateTime dateTime = DateTime.Now;

            int jobApproved =  await _context.Jobs.Where(u=> u.JobStatus== statusApproved && u.EmployerId== employerId).CountAsync();
            int jobPending = await _context.Jobs.Where(u => u.JobStatus == statusPending && u.EmployerId == employerId).CountAsync();
            int jobRejected = await _context.Jobs.Where(u => u.JobStatus == statusRejected && u.EmployerId == employerId).CountAsync();
            int jobExpire = await _context.Jobs.Where(u => u.ExpiredDate < dateTime && u.EmployerId == employerId).CountAsync();

            StatisticalGetStatusJob getJobStatus = new StatisticalGetStatusJob();
            getJobStatus.Approval = jobApproved;
            getJobStatus.Pending = jobPending;
            getJobStatus.Expired = jobExpire;
            getJobStatus.Reject = jobRejected;
            return getJobStatus;
        }

        public async Task<StatisticalGetTotalJobAndAppli> GetStatisticalTotalJobAndAppli(string employerId)
        {

            int totalJob = await _context.Jobs.Where(u=> u.EmployerId == employerId).CountAsync();
            int totalApply = await _context.Applications.Include(u=>u.Job).Where(a => a.Job.EmployerId == employerId && a.Status!=0).CountAsync();
            var totalUser = await _context.Applications.Include(u => u.Job).Where(a => a.Job.EmployerId == employerId && a.Status != 0).ToListAsync();
            int UserApply = await _context.Applications
                            .Where(a => a.Job.EmployerId == employerId && a.Status != 0)
                            .Select(a => a.UserId) // Chọn UserId
                            .Distinct()            // Loại bỏ các giá trị trùng lặp
                            .CountAsync();

            StatisticalGetTotalJobAndAppli gettotal = new StatisticalGetTotalJobAndAppli()
            {
                totalJob = totalJob,
                totalApply = totalApply,
                totalUser = UserApply
            };
            
            return gettotal;
        }


        public async Task<StatisticalGetApplyDateRange> GetApplyAndDateRange(string employerId,DateTime startDate, DateTime endDate)
        {
            var apply = await _context.Applications.Include(u => u.Job).Where(u => u.DateApply >= startDate 
            && u.DateApply <= endDate && u.Status != 0 && u.Job.EmployerId== employerId).CountAsync();
            var approve = await _context.Applications.Include(u => u.Job).Where(u => u.DateApply >= startDate && u.DateApply <= endDate && u.Status == 2 && u.Job.EmployerId == employerId).CountAsync();
            var reject = await _context.Applications.Include(u => u.Job).Where(u => u.DateApply >= startDate && u.DateApply <= endDate && u.Status == 3 && u.Job.EmployerId == employerId).CountAsync();
            return new StatisticalGetApplyDateRange
            {
                Apply = apply,
                Approve = approve,
                Reject = reject
            };
        }



        // admin
        public async Task<AdminGetTotal> GetStatisticalTotal()
        {

            int totalJob = await _context.Jobs.CountAsync();
            int totalApply = await _context.Applications.Where(a => a.Status != 0).CountAsync();
            var employerUsers = await _userManager.GetUsersInRoleAsync("Employer");
            var Users = await _userManager.GetUsersInRoleAsync("User");
            int Apply = await _context.Applications.Where(a=> a.Status != 0).CountAsync();
            AdminGetTotal gettotal = new AdminGetTotal()
            {
                employer = employerUsers.Count(),
                jobseeker = Users.Count(),
                jobpost = totalJob,
                apply = Apply

            };

            return gettotal;
        }


        public async Task<List<UserStatistics>> GetUserCountByRoleAndDateRange(string role, DateTime startDate, DateTime endDate)
        {
            var user = await _userManager.GetUsersInRoleAsync(role);
            var userdto=user.Where(u=>u.CreateAt >= startDate && u.CreateAt <= endDate)
                .GroupBy(u => u.CreateAt.Value.Date)
                .Select( g => new UserStatistics
                {
                    Date =  g.Key.ToString("dd/MM/yyyy"),
                    Count = g.Count()
                })
                .ToList();
            return userdto;
        }

        public async Task<List<UserStatistics>> GetJobCountAndDateRange( DateTime startDate, DateTime endDate)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var job = await _context.Jobs.Where(u => u.CreateOn >= startDate && u.CreateOn <= endDate && u.JobStatus== status)
                .GroupBy(u => u.CreateOn.Date )
                .Select(g => new UserStatistics
                {
                    Date = g.Key.ToString("dd/MM/yyyy"),
                    Count = g.Count()
                })
                .ToListAsync();
            return job;
        }
    }
}
