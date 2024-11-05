using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetData()
        {
            var data = new
            {
                Id = 1,
                Name = "Product 1",
                Price = 1000
            };

            return new JsonResult(data);
        }
    }
}
