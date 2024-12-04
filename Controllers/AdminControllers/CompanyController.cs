using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,UserManager<AppUser> userManager)
		{		
			_unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
		}
		public IActionResult Index()
        {
            //List<Company> companyList = _unitOfWork.CompanyRepo
            //.GetAll() // Áp dụng bộ lọc cho bảng có Status
            //.ToList();

            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company? company = new Company();
            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _unitOfWork.CompanyRepo.Get(x => x.Id == id);
                if (company == null)
                {
                    return NotFound();
                }
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company, IFormFile? file) {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                /* Xử lý file */
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string companyPath = Path.Combine(wwwRootPath, @"admin\img\Company");

                    using (var fileStream = new FileStream(Path.Combine(companyPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    company.Logo = @"\admin\img\Company\" + fileName;
                }
                if (company.Id == 0 || company.Id == null)
                {
                    /* add */
                    company.Create=DateTime.Now;
                    _unitOfWork.CompanyRepo.Add(company);
                    _unitOfWork.Save();
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    /* update */
                    company.Update= DateTime.Now;
                    _unitOfWork.CompanyRepo.Update(company);
                    _unitOfWork.Save();
                    TempData["success"] = "Company update successfully";
                }
                return RedirectToAction("Index", "Company");
            }
            return View(company);
        }

        public IActionResult Detail(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? company = _unitOfWork.CompanyRepo.Get(x => x.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companyList = _unitOfWork.CompanyRepo
              .GetAll()
              .Where(c => c.Status == true)
              .ToList();
            return Json(new {Data = companyList});
        }


        [HttpPut]
        public async Task<IActionResult> Hidden(int? id)
        {
            Company? company = _unitOfWork.CompanyRepo.Get(x => x.Id == id);
            company.Status = false;
            var users= await _userManager.Users.Where(user=>user.CompanyId == id).ToListAsync();
            foreach (var user in users)
            {
                user.Status = 0;
                await _userManager.UpdateAsync(user);                
            }
            _unitOfWork.CompanyRepo.Update(company);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpGet]
        public JsonResult IsPhoneAndEmailAvailable(string phone, string email)
        {
            var isPhoneTaken = _unitOfWork.CompanyRepo.Get(e => e.Phone == phone);
            var isEmailTaken = _unitOfWork.CompanyRepo.Get(e => e.Email == email);

            return Json(new { isPhoneTaken = isPhoneTaken != null, isEmailTaken = isEmailTaken != null });
        }
    }
}
