using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            
            return View();
        }


    }


}
