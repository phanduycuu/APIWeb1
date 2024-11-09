using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;

namespace APIWeb1.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDBContext _context;
        public ApplicationRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Application> CreateAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return application;
        }
    }
}
