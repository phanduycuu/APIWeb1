using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using APIWeb1.Models;
using APIWeb1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.AdminControllers
{

    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public AccountController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employer()
        {
            return View();
        }

        public async Task<IActionResult> DetailEmployer(string id)
        {
            if (id==null )
            {
                return NotFound();
            }
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        public async Task<IActionResult> DetailUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser? user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployer()
        {
            var employerlist = await _unitOfWork.AccoutAdminRepo.GetAllAsync("Employer");
            var employerDtos= employerlist.Select(u=>u.ToAccountAdminDto()).ToList();
            return Json(new { Data = employerDtos });
        }

        public async Task<IActionResult> AcceptEmployer(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id,1);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been authenticed successfully.</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                TempData["success"] = "Confirm successfully";
                return RedirectToAction("Employer", "Account");
            }
            else
            {
                TempData["success"] = "Confirm failed";
                return RedirectToAction("Employer", "Account"); }
        }

        public async Task<IActionResult> RefuseEmployer(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 2);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been refused." +
                    "We cannot confirm that you are an employee of the company you registered with." +
                    " Please contact with your company to have more detail</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                TempData["success"] = "Confirm  successfully";
                return RedirectToAction("Employer", "Account");
            }
            else
            {
                TempData["success"] = "Confirm failed";
                return RedirectToAction("Employer", "Account");
            }
        }
        public IActionResult JobSeekers()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var userlist = await _unitOfWork.AccoutAdminRepo.GetAllAsync("User");
            var userDtos = userlist.Select(u => u.ToAccountAdminDto()).ToList();
            return Json(new { Data = userDtos });
        }

        public async Task<IActionResult> AcceptUser(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 1);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been authenticed successfully.</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                TempData["success"] = "Confirm successfully";
                return RedirectToAction("JobSeekers", "Account");
            }
            else
            {
                TempData["success"] = "Confirm failed";
                return RedirectToAction("JobSeekers", "Account");
            }
        }

        public async Task<IActionResult> RefuseUser(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 2);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been refused." +
                    "We found that your account has violated the rules, or has unusual statuses." +
                    " We will resolve this issue soon. please be patient</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                TempData["success"] = "Confirm  successfully";
                return RedirectToAction("JobSeekers", "Account");
            }
            else
            {
                TempData["success"] = "Confirm failed";
                return RedirectToAction("JobSeekers", "Account");
            }
        }

    }
}
