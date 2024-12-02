using APIWeb1.Data;
using APIWeb1.Dtos.Admin;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    Role = roles.FirstOrDefault() // Lấy role đầu tiên nếu có
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
                    Role = roles.FirstOrDefault() // Lấy role đầu tiên nếu có
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
                Role = roles.FirstOrDefault() // Lấy role đầu tiên nếu có
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
                company= company.Skip(skipNumber).Take(query.PageSize);
            }
            
            var companymodel= await company.ToListAsync();
            return new PaginationGetAllCompany
            {
                Company=companymodel,
                Total= Total.Count(),

            };
        }
    }
}
