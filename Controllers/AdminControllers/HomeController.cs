using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.AdminControllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }


}
