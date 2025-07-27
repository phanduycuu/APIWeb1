using APIWeb1.Dtos.AppUsers;
using APIWeb1.Extensions;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.AdminControllers
{

    public class ProfileAdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public ProfileAdminController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            var username = User.GetUsername();
            var userInfo = await _userManager.Users
                .Include(u => u.Company).Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.UserName == username);
            var user = new Adminprofile 
            {
                Id=userInfo.Id,
                Fullname= userInfo.Fullname,
                PhoneNumber= userInfo.PhoneNumber,
                Sex=userInfo.Sex,
                Birthdate=userInfo.Birthdate,
                Email= userInfo.Email
            };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Adminprofile appUser)
        {
            if (ModelState.IsValid)
            {
                //var username = User.GetUsername();
                var userInfo = await _userManager.FindByIdAsync(appUser.Id);
                userInfo.Fullname = appUser.Fullname;
                userInfo.PhoneNumber = appUser.PhoneNumber;
                userInfo.Birthdate = appUser.Birthdate;
                userInfo.Sex = appUser.Sex;
                await _userManager.UpdateAsync(userInfo);
                return RedirectToAction("Index", "ProfileAdmin");
            }
            TempData["success"] = "Blog update fail";
            return View(appUser);
        }
    }
}
