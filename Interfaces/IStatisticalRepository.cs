using APIWeb1.Dtos.Job;
using APIWeb1.Dtos.Statisticals;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IStatisticalRepository
    {
        Task<StatisticalGetStatusJob> GetStatisticalJobStatus(string employerId);
        Task<StatisticalGetTotalJobAndAppli> GetStatisticalTotalJobAndAppli(string employerId);

        //admin
        Task<AdminGetTotal> GetStatisticalTotal();
        Task<List<UserStatistics>> GetUserCountByRoleAndDateRange(string role, DateTime startDate, DateTime endDate);
        Task<List<UserStatistics>> GetJobCountAndDateRange(DateTime startDate, DateTime endDate);
    }
}
