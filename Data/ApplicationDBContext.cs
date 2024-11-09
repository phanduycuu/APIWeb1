using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Application> Applications { get; set; }

        //public DbSet<Skill> Skills { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobSkill>(x => x.HasKey(p => new { p.JobId, p.SkillId }));

            modelBuilder.Entity<JobSkill>()
                .HasOne(u => u.Job)
                .WithMany(u => u.JobSkills)
                .HasForeignKey(p => p.JobId);
            modelBuilder.Entity<JobSkill>()
                .HasOne(u => u.Skill)
                .WithMany(u => u.JobSkills)
                .HasForeignKey(p => p.SkillId);

            modelBuilder.Entity<Application>().HasKey(a => a.Id);
            modelBuilder.Entity<Application>()
                .HasOne(u => u.Job)
                .WithMany(u => u.Applications)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Application>()
                .HasOne(u => u.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Employer",
                    NormalizedName = "EMPLOYER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = 8,
                    Name= ".Net",
                     
                },
                new Skill
                {
                    Id = 9,
                    Name = "Java",
                     
                },
                new Skill
                {
                    Id = 10,
                    Name = "Spring boot",
                     
                }
            );
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "FPT",
                    Description="Công ti về công nghệ hàng đầu thế giới",
                    Email="FPT@gmail.com",
                    Phone="0368166471",
                    Logo="/abc.png",
                    Website="FPT.com",
                    Create= DateTime.Now,
                    Industry="Information technology",
                    Location="HCM",
                    Update= DateTime.Now,
                    Status= true

                },
                new Company
                {
                    Id = 2,
                    Name = "BOSCH",
                    Description = "Đa lĩnh vực",
                    Email = "BOSCH@gmail.com",
                    Phone = "0368166471",
                    Logo = "/xyz.png",
                    Website = "BOSCH.com",
                    Create = DateTime.Now,
                    Industry = "Information technology",
                    Location = "HCM",
                    Update = DateTime.Now,
                    Status = true
                }
            );

        }

    }
}
