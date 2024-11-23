using APIWeb1.Data;
using APIWeb1.Extensions;
using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/statistical")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public StatisticalController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)

        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;


        }

        [HttpGet("Get-Statistical-JobStatus")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetStatisticalJobStatus()
        {
            var username = User.GetUsername();
            var userInfo = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
            var statistical = await _unitOfWork.StatisticalRepo.GetStatisticalJobStatus(userInfo.Id);
            return Ok(statistical);
        }

        [HttpGet("Get-Statistical-TotalJobAndAppli")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetStatisticalTotalJobAndAppli()
        {
            var username = User.GetUsername();
            var userInfo = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
            var statistical = await _unitOfWork.StatisticalRepo.GetStatisticalTotalJobAndAppli(userInfo.Id);
            return Ok(statistical);
        }
    }
}
