using APIWeb1.Dtos.AppUsers;
using APIWeb1.Extensions;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(UserManager<AppUser> userManager, ITokenRepository tokenRepository, SignInManager<AppUser> signInManager, IConfiguration configuration, IEmailSender emailSender,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _signinManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-AllAccount")]
        //[Authorize]
        public async Task<IActionResult> GetAllAccount([FromQuery] AdminAccountQuery query)
        {
            var user = await _unitOfWork.AccoutAdminRepo.GetAllAccount(query);
            
            return Ok(user);
        }

        [HttpGet("Get-Account-UserOrEmployer")]
        //[Authorize]
        public async Task<IActionResult> GetUserOrEmployer([FromQuery] AdminAccountQuery query,string role)
        {
            var user = await _unitOfWork.AccoutAdminRepo.GetUserOrEmployer(query, role);

            return Ok(user);
        }

        [HttpGet("Get-ById")]
        //[Authorize]
        public async Task<IActionResult> GetById(string Id)
        {
            var user = await _unitOfWork.AccoutAdminRepo.GetById(Id);

            return Ok(user);
        }


        [HttpPost("Accept-Account-Employer")]
        public async Task<IActionResult> AcceptEmployer(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 1);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been authenticed successfully.</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                return Ok("Update successfully");
            }
            else
            {

                return Ok("Update fail");
            }
        }
        [HttpPost("Refuse-Account-Employer")]
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
                return Ok("Update successfully");
            }
            else
            {
                return Ok("Update fail");
            }
        }

        [HttpPost("Accept-Account-User")]
        public async Task<IActionResult> AcceptUser(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 1);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been authenticed successfully.</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                return Ok("Update successfully");
            }
            else
            {
                return Ok("Update fail");
            }
        }

        [HttpPost("Refuse-Account-User")]
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
                return Ok("Update successfully");
            }
            else
            {
                return Ok("Update fail");
            }
        }

        [HttpGet("Get-All-Company")]
        //[Authorize]
        public async Task<IActionResult> GetAllCompany([FromQuery] CompanyQueryObj query)
        {
            var company = await _unitOfWork.AccoutAdminRepo.GetAllCompany(query);

            return Ok(company);
        }
    }
}
