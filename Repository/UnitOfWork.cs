using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;

namespace APIWeb1.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _context;
        private  UserManager<AppUser> _userManager;
        private  RoleManager<IdentityRole> _roleManager;
        public ICompanyRepository CompanyRepo { get; private set; }
        public IJobRepository JobRepo { get; private set; }

        public IJobSkillRepository JobSkillRepo { get; private set; }

        public ISkillRepository SkillRepo { get; private set; }

        public IApplicationRepository ApplicationRepo { get; private set; }

        public IAccoutAdminRepository AccoutAdminRepo { get; private set; }

        public IAddressRepository AddressRepo { get; private set; }

        public UnitOfWork(ApplicationDBContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            CompanyRepo = new CompanyRepository(_context);   
            JobRepo = new JobRepository(_context);
            JobSkillRepo = new JobSkillRepository(_context);
            SkillRepo = new SkillRepository(_context);
            ApplicationRepo = new ApplicationRepository(_context);
            AccoutAdminRepo = new AccoutAdminRepository(_userManager, _roleManager, _context);
            AddressRepo = new AddressRepository(_context);

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
