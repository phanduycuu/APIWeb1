using APIWeb1.Data;
using APIWeb1.Dtos.Admin;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Blogs;
using APIWeb1.Dtos.Companys;
using APIWeb1.Dtos.Job;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;

namespace APIWeb1.Repository
{
    public class AccoutAdminRepository : IAccoutAdminRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _context;
        public AccoutAdminRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<List<AppUser>> GetAllAsync(string role)
        {
            var employerUsers = await _userManager.GetUsersInRoleAsync(role);

            // Sau đó lấy lại danh sách từ DbContext để có thể sử dụng Include
            var usersWithCompany = await _context.Users
                .Where(user => employerUsers.Select(e => e.Id).Contains(user.Id)) // Lọc các user là Employer
                .Include(user => user.Company) // Eager load Company
                .ToListAsync();
            return usersWithCompany.ToList();
        }
        public async Task<bool> UpdateAsync(string userId,int status)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);
            if (existingUser == null)
                return false;

            existingUser.Status = status;

            var result = await _userManager.UpdateAsync(existingUser);
            _context.SaveChanges();
            return result.Succeeded;
        }



        // Api account Admin
        public async Task<PaginationGetAllAccount> GetAllAccount(AdminAccountQuery query)
        {
            var usersQuery = _context.Users
            .Include(user => user.Company)
            .Include(user => user.Address)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Fullname))
            {
                usersQuery = usersQuery.Where(s => s.Fullname.Contains(query.Fullname));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Fullname", StringComparison.OrdinalIgnoreCase))
                {
                    usersQuery = query.IsDecsending ? usersQuery.OrderByDescending(s => s.Fullname) : usersQuery.OrderBy(s => s.Fullname);
                }
            }

            var Total = await usersQuery.ToListAsync();

            if (query.PageSize != 0 && query.PageNumber != 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                usersQuery = usersQuery.Skip(skipNumber).Take(query.PageSize);
            }

            // Thực thi truy vấn để tải dữ liệu vào bộ nhớ
            var users = await usersQuery.ToListAsync();

            var userDtos = new List<AdminAccountUser>();

            // Lấy từng User và thêm thông tin Roles
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Lấy danh sách Roles của User

                userDtos.Add(new AdminAccountUser
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    Img = user.Img,
                    Phone = user.PhoneNumber,
                    Username = user.UserName,
                    Email = user.Email,
                    Companyname = user.Company?.Name,
                    Sex = user.Sex,
                    Birthdate = user.Birthdate,
                    Address = user.Address,
                    Role = roles.FirstOrDefault() ,// Lấy role đầu tiên nếu có
                    Status = user.Status
                });
            }

            return new PaginationGetAllAccount
            {
                AccountUser=userDtos,
                Total= Total.Count()
            };

        }

        public async Task<PaginationGetAllAccount> GetUserOrEmployer(AdminAccountQuery query, string role)
        {
            var employerUsers = await _userManager.GetUsersInRoleAsync(role);

            // Sau đó lấy lại danh sách từ DbContext để có thể sử dụng Include
            var usersQuery = _context.Users
                .Where(user => employerUsers.Select(e => e.Id).Contains(user.Id)) // Lọc các user là Employer
                .Include(user => user.Company)
                .Include(user => user.Address)// Eager load Company
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Fullname))
            {
                usersQuery = usersQuery.Where(s => s.Fullname.Contains(query.Fullname));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Fullname", StringComparison.OrdinalIgnoreCase))
                {
                    usersQuery = query.IsDecsending ? usersQuery.OrderByDescending(s => s.Fullname) : usersQuery.OrderBy(s => s.Fullname);
                }
            }
            var Total = await usersQuery.ToListAsync();
            if (query.PageSize != 0 && query.PageNumber != 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                usersQuery = usersQuery.Skip(skipNumber).Take(query.PageSize);
            }

            // Thực thi truy vấn để tải dữ liệu vào bộ nhớ
            var users = await usersQuery.ToListAsync();

            var userDtos = new List<AdminAccountUser>();

            // Lấy từng User và thêm thông tin Roles
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Lấy danh sách Roles của User

                userDtos.Add(new AdminAccountUser
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    Img = user.Img,
                    Phone = user.PhoneNumber,
                    Username = user.UserName,
                    Email = user.Email,
                    Companyname = user.Company?.Name,
                    Sex = user.Sex,
                    Birthdate = user.Birthdate,
                    Address = user.Address,
                    Role = roles.FirstOrDefault(), // Lấy role đầu tiên nếu có
                     Status = user.Status
                });
            }

            return new PaginationGetAllAccount
            {
                AccountUser = userDtos,
                Total = Total.Count()
            };
        }
        public async Task<AdminAccountUser> GetById(string Id)
        {
            var user = await _context.Users
            .Include(user => user.Company)
            .Include(user => user.Address)
            .Where(user => user.Id == Id).FirstOrDefaultAsync();

            var roles = await _userManager.GetRolesAsync(user); 

            return new AdminAccountUser
            {
                Id = Id,
                Fullname = user.Fullname,
                Img = user.Img,
                Phone = user.PhoneNumber,
                Username = user.UserName,
                Email = user.Email,
                Companyname = user.Company?.Name,
                Sex = user.Sex,
                Birthdate = user.Birthdate,
                Address = user.Address,
                Role = roles.FirstOrDefault(), // Lấy role đầu tiên nếu có
                Status = user.Status
            };


        }

        // Company

        public async Task<PaginationGetAllCompany> GetAllCompany(CompanyQueryObj query)
        {
            var company = _context.Companys.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                company = company.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Industry))
            {
                company = company.Where(s => s.Industry.Contains(query.Industry));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    company = query.IsDecsending ? company.OrderByDescending(s => s.Name) : company.OrderBy(s => s.Name);
                }
            }
            var Total = await company.ToListAsync();
            if (query.PageSize != 0 && query.PageNumber != 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                company = company.Skip(skipNumber).Take(query.PageSize);
            }

            var companymodel = await company.ToListAsync();
            return new PaginationGetAllCompany
            {
                Company = companymodel,
                Total = Total.Count(),

            };
        }

        public async Task<PaginationGetAllBlog> GetAllBlog(BlogQueryObject query)
        {
            var blogs = _context.Blogs.Include(u => u.User).ThenInclude(p => p.Company).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                blogs = blogs.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    blogs = query.IsDecsending ? blogs.OrderByDescending(s => s.Title) : blogs.OrderBy(s => s.Title);
                }
            }
            var Total = await blogs.ToListAsync();
            if (query.PageSize != 0 && query.PageNumber != 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                blogs = blogs.Skip(skipNumber).Take(query.PageSize);
            }

            var blogmodel = await blogs.Select(u => new GetAllBlogDto
            {
                Id = u.Id,
                Username = u.User.Fullname,
                Companyname = u.User.Company.Name,
                Img = u.Img,
                Title = u.Title,
                Content = u.Content,
                CreateOn = u.CreateAt,
                IsShow = u.IsShow,
                Status = u.Status
            }).ToListAsync();
            return new PaginationGetAllBlog
            {
                Blogs = blogmodel,
                Total = Total.Count(),

            };
        }

        //job
        public async Task<PaginationGetAllJob> GetAllJob(JobQueryObject query)
        {
            var job = _context.Jobs.Include(a => a.Employer).ThenInclude(b => b.Company).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreateOn", StringComparison.OrdinalIgnoreCase))
                {
                    job = query.IsDecsending ? job.OrderByDescending(s => s.CreateOn) : job.OrderBy(s => s.CreateOn);
                }
            }
            var Total = await job.ToListAsync();
            if (query.PageSize != 0 && query.PageNumber != 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                job = job.Skip(skipNumber).Take(query.PageSize);
            }

            var jobmodel = await job.Select(job => new GetAllJobDto
            {
                Id = job.Id,
                Title = job.Title,
                Salary = job.Salary,
                CreateOn = job.CreateOn,
                Employer = new GetEmployer
                {
                    Id = job.Employer.Id,
                    Company = new GetCompany
                    {
                        Name = job.Employer.Company.Name,
                        logo = job.Employer.Company.Logo
                    },
                },
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                IsShow = job.IsShow,
                LocationShort = job.Address.Province + ", " + job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            }).ToListAsync();
            return new PaginationGetAllJob
            {
                Jobs = jobmodel,
                Total = Total.Count(),

            };
        }

        // Skill

        public async Task<PaginationGetAllSkill> GetAllSkill(SkillQuery query)
        {
            var skills = _context.Skills.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                skills = skills.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    skills = query.IsDecsending ? skills.OrderByDescending(s => s.Name) : skills.OrderBy(s => s.Name);
                }
            }
            var Total = await skills.ToListAsync();
            if (query.PageSize != 0 && query.PageNumber != 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                skills = skills.Skip(skipNumber).Take(query.PageSize);
            }

            var skillmodel = await skills.Select(u => new SkillDto
            {
                Id = u.Id,
                Name = u.Name,
                IsDelete = u.IsDeleted
            }).ToListAsync();
            return new PaginationGetAllSkill
            {
                Skills = skillmodel,
                Total = Total.Count(),

            };
        }
    }
}
