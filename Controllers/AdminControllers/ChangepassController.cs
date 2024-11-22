using APIWeb1.Dtos.Account;
using APIWeb1.Extensions;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.AdminControllers
{
    public class ChangepassController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ChangepassController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ChangePass([FromBody] ChangePass model)
        {
            var username = User.GetUsername();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurentPass, model.NewPass);
            if (!result.Succeeded)
            {
                // Trả về lỗi nếu mật khẩu không thỏa mãn
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok("Password changed successfully");

        }
    }
}
