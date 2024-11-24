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

        [HttpGet("GetUserStatistics")]
        public async Task<IActionResult> GetUserStatistics(DateTime startDate, DateTime endDate)
        {
            // Giả sử bạn có repository để lấy dữ liệu người dùng
            var jobSeekers = await _unitOfWork.StatisticalRepo.GetUserCountByRoleAndDateRange("User", startDate, endDate);
            var employers = await _unitOfWork.StatisticalRepo.GetUserCountByRoleAndDateRange("Employer", startDate, endDate);

            // Xử lý dữ liệu theo từng ngày
            var labels = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                    .Select(offset => startDate.AddDays(offset).ToString("dd/MM/yyyy"))
                                    .ToList();

            var jobSeekerData = labels.Select(date => jobSeekers.FirstOrDefault(js => js.Date == date)?.Count ?? 0).ToList();
            var employerData = labels.Select(date => employers.FirstOrDefault(em => em.Date == date)?.Count ?? 0).ToList();

            //Trả về JSON
            return Ok(new
            {
                labels,
                jobSeekerData,
                employerData
            });
        }
    }
}
