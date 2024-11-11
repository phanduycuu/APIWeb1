using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
    }
}
