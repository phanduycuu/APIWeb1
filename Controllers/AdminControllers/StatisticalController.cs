using APIWeb1.Interfaces;
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
    }
}
