using APIWeb1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UserInfoViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return Content("User not logged in");

            var userInfo = await _userManager.Users
                .Include(u => u.Company).Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.UserName == username);

            return View(userInfo);
        }
    }

}
