using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.AdminControllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employer()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployer()
        {
            List<AppUser> employerlist = await _unitOfWork.AccoutAdminRepo.GetAllAsync("Employer");
            return Json(new { Data = employerlist });
        }

        public async Task<IActionResult> UpdateUser(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id,1);
            if (isUpdated)
            {
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Employer", "Account");
            }
            else
            {
                TempData["success"] = "Company created faile";
                return RedirectToAction("Employer", "Account"); }
        }
        public IActionResult JobSeekers()
        {
            return View();
        }

    }
}
