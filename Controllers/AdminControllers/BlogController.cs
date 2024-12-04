using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models.Enum;
using APIWeb1.Models;
using Microsoft.AspNetCore.Mvc;
using APIWeb1.Dtos.Blogs;
using Microsoft.AspNetCore.Authorization;

namespace APIWeb1.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            List<BlogAdminDto> BlogList = await _unitOfWork.BlogRepo.GetAdminBlog();
            return Json(new { Data = BlogList });
        }
        public async Task<IActionResult> Accept(int Id)
        {
            var blog = await _unitOfWork.BlogRepo.GetByIdForAll(Id);
            blog.Status = 1;
            blog.IsShow = true;
            await _unitOfWork.BlogRepo.UpdateEmployerblog(blog);
            TempData["success"] = "Blog update successfully";
            return RedirectToAction("Index", "Blog");
        }
        public async Task<IActionResult> Refuse(int Id)
        {
            var blog = await _unitOfWork.BlogRepo.GetByIdForAll(Id);
            blog.Status = 2;
            blog.IsShow = false;
            await _unitOfWork.BlogRepo.UpdateEmployerblog(blog);
            TempData["success"] = "Blog update successfully";
            return RedirectToAction("Index", "Blog");
        }

        public IActionResult Detail(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Blog? blog = _unitOfWork.BlogRepo.Get(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
    }
}

