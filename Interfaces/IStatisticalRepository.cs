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
    }
}
