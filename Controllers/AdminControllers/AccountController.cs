using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.AdminControllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employer()
        {
            return View();
        }

        public IActionResult JobSeekers()
        {
            return View();
        }

    }
}
