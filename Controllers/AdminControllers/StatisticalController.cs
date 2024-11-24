using APIWeb1.Dtos.Statisticals;
using APIWeb1.Interfaces;
using APIWeb1.Models;
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

        

    }
}
