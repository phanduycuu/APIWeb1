using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.AdminControllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }


}
