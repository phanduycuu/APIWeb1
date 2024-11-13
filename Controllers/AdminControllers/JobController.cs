using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APIWeb1.Controllers.AdminControllers
{
    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            List<JobAdminDto> JobList = await _unitOfWork.JobRepo.GetAdminJob();
            return Json(new { Data = JobList });
        }
        public async Task<IActionResult> Accept(int Id)
        {
            var status= EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            _unitOfWork.JobRepo.UpdateStatusJob(Id, status);
            TempData["success"] = "Job update successfully";
            return RedirectToAction("Index", "Job");
        }
        public async Task<IActionResult> Refuse(int Id)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Rejected");
            _unitOfWork.JobRepo.UpdateStatusJob(Id, status);
            TempData["success"] = "Job update successfully";
            return RedirectToAction("Index", "Job");
        }

        public IActionResult Detail(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Job? job = _unitOfWork.JobRepo.Get(x => x.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }
    }
}
