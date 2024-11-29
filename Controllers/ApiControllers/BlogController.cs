using APIWeb1.Data;
using APIWeb1.Dtos.Blogs;
using APIWeb1.Dtos.Job;
using APIWeb1.Extensions;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public BlogController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Create([FromForm] CreateBlogDto blogDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var filePath = "";
            if (blogDto.Img == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                filePath = Path.Combine(@"wwwroot\admin\img\Blog\", $"{appUser.Id}_{blogDto.Img.FileName}");

                // Lưu file vào hệ thống file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await blogDto.Img.CopyToAsync(stream);
                }
                Blog blogmodel = new Blog() 
                {
                    UserId= appUser.Id,
                    Img= @"\admin\img\Blog\"+$"{appUser.Id}_{blogDto.Img.FileName}",
                    Title=blogDto.Title,
                    Content=blogDto.Content,
                    CreateAt= DateTime.Now,
                    UpdatedAt= null,
                    Status=0
                };
                await _unitOfWork.BlogRepo.CreateAsync(blogmodel);
                return Created();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetBlog([FromQuery] BlogQueryObject query)
        {
            var blog = await _unitOfWork.BlogRepo.GetAllAsync(query);
            return Ok(blog);
        }

        [HttpGet("GetForEmployer")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetBlogForEmployer([FromQuery] BlogQueryObject query)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var blog = await _unitOfWork.BlogRepo.GetForEmployer(query, appUser.Id);
            return Ok(blog);
        }

        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById(int blogId)
        {
            var blog = await _unitOfWork.BlogRepo.GetByIdForAll(blogId);
            return Ok(blog);
        }

        [HttpGet("GetBlogByIdForEmployer")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetBlogByIdForEmployer(int blogId)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var blog = await _unitOfWork.BlogRepo.GetByIdForEmployer(blogId, appUser.Id);
            if (blog == null)
            {
                return BadRequest("you don't have permition for this blog");
            }
            return Ok(blog);
        }


        [HttpGet("GetTotal")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetTotal([FromQuery] BlogQueryObject query)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var total = await _unitOfWork.BlogRepo.GetTotalAsync(query, appUser.Id);
            return Ok(total);
        }

        [HttpGet("GetTotalWithConditions")]
        public async Task<IActionResult> GetTotalWithConditionsAsync([FromQuery] BlogQueryObject query)
        {
            var total = await _unitOfWork.BlogRepo.GetTotalWithConditions(query);
            return Ok(total);
        }

        [HttpPost("Update-IsShow-Blog")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateEmployerIsShowBlog(IsShowBlog dto) // status= 2 duyet, status= 3 tu choi
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var blogdto = await _unitOfWork.BlogRepo.GetByIdForEmployer(dto.BlogId, appUser.Id);

            if (blogdto == null)
            {

                return BadRequest("You don't have permition for this blog");

            }
            else
            {
                Blog blog = blogdto;
                blog.IsShow = dto.IsShow;
                await _unitOfWork.BlogRepo.UpdateEmployerblog(blog);
                return Ok("Update status successfully");
            }
        }
    }

}
