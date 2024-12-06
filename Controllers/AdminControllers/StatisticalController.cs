using APIWeb1.Dtos.Statisticals;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.AdminControllers
{

    public class StatisticalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatisticalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> GetTotal()
        {

            AdminGetTotal total = await _unitOfWork.StatisticalRepo.GetStatisticalTotal();

            return Json(new { Data = total });
        }

        //[HttpGet("GetUserStatistics")]
        [HttpPost]
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
            return Json(new
            {
                Data = new
                {
                    labels,
                    jobSeekerData,
                    employerData
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetJobStatistics(DateTime startDate, DateTime endDate)
        {
            // Giả sử bạn có repository để lấy dữ liệu người dùng
            var job = await _unitOfWork.StatisticalRepo.GetJobCountAndDateRange(startDate, endDate);

            // Xử lý dữ liệu theo từng ngày
            var labels = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                    .Select(offset => startDate.AddDays(offset).ToString("dd/MM/yyyy"))
                                    .ToList();

            var jobData = labels.Select(date => job.FirstOrDefault(js => js.Date == date)?.Count ?? 0).ToList();

            //Trả về JSON
            return Json(new
            {
                Data = new
                {
                    labels,
                    jobData
                }
            });
        }

    }
}
