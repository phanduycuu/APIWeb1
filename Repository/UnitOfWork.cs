using APIWeb1.Data;
using APIWeb1.Interfaces;

namespace APIWeb1.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _context;
        public ICompanyRepository CompanyRepo { get; private set; }
        public IJobRepository JobRepo { get; private set; }

        public IJobSkillRepository JobSkillRepo { get; private set; }

        public ISkillRepository SkillRepo { get; private set; }

        public IApplicationRepository ApplicationRepo { get; private set; }

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            CompanyRepo = new CompanyRepository(_context);   
            JobRepo = new JobRepository(_context);
            JobSkillRepo = new JobSkillRepository(_context);
            SkillRepo = new SkillRepository(_context);
            ApplicationRepo = new ApplicationRepository(_context);

        }
    }
}
