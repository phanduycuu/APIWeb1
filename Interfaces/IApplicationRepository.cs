using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application> CreateAsync(Application application);
    }
}
