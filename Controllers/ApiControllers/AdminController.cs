using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Blogs;
using APIWeb1.Dtos.Companys;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Dtos.Statisticals;
using APIWeb1.Extensions;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(UserManager<AppUser> userManager, ITokenRepository tokenRepository, SignInManager<AppUser> signInManager, IConfiguration configuration, IEmailSender emailSender,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _signinManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Get-AllAccount")]
        //[Authorize]
        public async Task<IActionResult> GetAllAccount([FromQuery] AdminAccountQuery query)
        {
            var user = await _unitOfWork.AccoutAdminRepo.GetAllAccount(query);
            
            return Ok(user);
        }

        [HttpGet("Get-Account-UserOrEmployer")]
        //[Authorize]
        public async Task<IActionResult> GetUserOrEmployer([FromQuery] AdminAccountQuery query,string role)
        {
            var user = await _unitOfWork.AccoutAdminRepo.GetUserOrEmployer(query, role);

            return Ok(user);
        }

        [HttpGet("Get-ById")]
        //[Authorize]
        public async Task<IActionResult> GetById(string Id)
        {
            var user = await _unitOfWork.AccoutAdminRepo.GetById(Id);

            return Ok(user);
        }


        [HttpPost("Accept-Account-Employer")]
        public async Task<IActionResult> AcceptEmployer(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 1);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been authenticed successfully.</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                return Ok("Update successfully");
            }
            else
            {

                return Ok("Update fail");
            }
        }
        [HttpPost("Refuse-Account-Employer")]
        public async Task<IActionResult> RefuseEmployer(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 2);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been refused." +
                    "We cannot confirm that you are an employee of the company you registered with." +
                    " Please contact with your company to have more detail</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                return Ok("Update successfully");
            }
            else
            {
                return Ok("Update fail");
            }
        }

        [HttpPost("Accept-Account-User")]
        public async Task<IActionResult> AcceptUser(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 1);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been authenticed successfully.</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                return Ok("Update successfully");
            }
            else
            {
                return Ok("Update fail");
            }
        }

        [HttpPost("Refuse-Account-User")]
        public async Task<IActionResult> RefuseUser(string id)
        {
            bool isUpdated = await _unitOfWork.AccoutAdminRepo.UpdateAsync(id, 2);
            if (isUpdated)
            {
                var user = await _userManager.FindByIdAsync(id);
                string subject = "Web Job Update Notification";
                string htmlMessage = "<p>Your account has been refused." +
                    "We found that your account has violated the rules, or has unusual statuses." +
                    " We will resolve this issue soon. please be patient</p>";

                await _emailSender.SendEmailAsync(user.Email, subject, htmlMessage);
                return Ok("Update successfully");
            }
            else
            {
                return Ok("Update fail");
            }
        }

        [HttpGet("Get-All-Company")]
        //[Authorize]
        public async Task<IActionResult> GetAllCompany([FromQuery] CompanyQueryObj query)
        {
            var company = await _unitOfWork.AccoutAdminRepo.GetAllCompany(query);

            return Ok(company);
        }

        [HttpGet("Get-ById-Company")]
        public IActionResult GetByIdCompany(int id)
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
            return Ok(company);
        }

        [HttpPost("Upsert-Company")]
        public async Task<IActionResult> Upsert([FromForm] AdminAddCompany dto)
        {
            if (ModelState.IsValid)
            {
                string filePath = "";

                    filePath = Path.Combine(@"wwwroot\admin\img\Company\", $"{dto.Img.FileName}");

                    // Lưu file vào hệ thống file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Img.CopyToAsync(stream);
                    }

                if (dto.Id == 0 || dto.Id == null)
                {
                    Company company = dto.ToCompanyFromAdminAddCompany();
                    company.Create = DateTime.Now;
                    company.Logo = @"\admin\img\Company\"+ $"{dto.Img.FileName}";
                    _unitOfWork.CompanyRepo.Add(company);
                    _unitOfWork.Save();
                }
                else
                {
                    Company companymodel = _unitOfWork.CompanyRepo.Get(x => x.Id == dto.Id);
                    companymodel.Name = dto.Name;
                    companymodel.Description = dto.Description;
                    companymodel.Phone = dto.Phone;
                    companymodel.Email = dto.Email;
                    companymodel.Website = dto.Website;
                    companymodel.Industry = dto.Industry;
                    companymodel.Update = DateTime.Now;
                    companymodel.Logo = @"\admin\img\Company\" + $"{dto.Img.FileName}";
                    _unitOfWork.CompanyRepo.Update(companymodel);
                    _unitOfWork.Save();
                }
                
            }
            return Ok();
        }

        [HttpPost("Update-Status-Company")]
        public async Task<IActionResult> Hidden(int id,bool status)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            } 
            Company company = _unitOfWork.CompanyRepo.Get(x => x.Id == id);
            company.Status = status;
            if (status == false)
            {
                var users = await _userManager.Users.Where(user => user.CompanyId == id).ToListAsync();
                foreach (var user in users)
                {
                    user.Status = 0;
                    await _userManager.UpdateAsync(user);
                }
            }
            _unitOfWork.CompanyRepo.Update(company);
            _unitOfWork.Save();

            return Ok("Update successfully");
        }

        // blog
        [HttpGet("Get-All-Blog")]
        //[Authorize]
        public async Task<IActionResult> GetAllBlog([FromQuery] BlogQueryObject query)
        {
            var blog = await _unitOfWork.AccoutAdminRepo.GetAllBlog(query);

            return Ok(blog);
        }

        [HttpPost("Add-Blogs")]
        //[Authorize(Roles = "Admin")]
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
                    UserId = appUser.Id,
                    Img = @"\admin\img\Blog\" + $"{appUser.Id}_{blogDto.Img.FileName}",
                    Title = blogDto.Title,
                    Content = blogDto.Content,
                    CreateAt = DateTime.Now,
                    UpdatedAt = null,
                    Status = 1,
                    IsShow = true,
                };
                await _unitOfWork.BlogRepo.CreateAsync(blogmodel);
                return Created();
            }

        }

        [HttpPost("Accept-Blogs")]
        public async Task<IActionResult> Accept(int Id)
        {
            var blog = await _unitOfWork.BlogRepo.GetByIdForAll(Id);
            blog.Status = 1;
            blog.IsShow = true;
            await _unitOfWork.BlogRepo.UpdateEmployerblog(blog);
            return Ok("Confirm successfully");
        }

        [HttpPost("Refuse-Blogs")]
        public async Task<IActionResult> Refuse(int Id)
        {
            var blog = await _unitOfWork.BlogRepo.GetByIdForAll(Id);
            blog.Status = 2;
            blog.IsShow = false;
            await _unitOfWork.BlogRepo.UpdateEmployerblog(blog);
            return Ok("Confirm successfully");
        }


        // Thong ke
        [HttpGet("Statistical-Get-Total")]
        public async Task<IActionResult> GetTotal()
        {

            AdminGetTotal total = await _unitOfWork.StatisticalRepo.GetStatisticalTotal();

            return Ok(total);
        }

        //[HttpGet("GetUserStatistics")]
        [HttpPost("Statistical-Get-Register-User")]
        public async Task<IActionResult> GetUserStatistics(DateTime startDate, DateTime endDate)
        {
            // Giả sử bạn có repository để lấy dữ liệu người dùng
            var jobSeekers = await _unitOfWork.StatisticalRepo.GetUserCountByRoleAndDateRange("User", startDate, endDate);
            var employers = await _unitOfWork.StatisticalRepo.GetUserCountByRoleAndDateRange("Employer", startDate, endDate);

            // Xử lý dữ liệu theo từng ngày
            var labels = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                    .Select(offset => startDate.AddDays(offset).ToString("dd/MM/yyyy"))
                                    .ToList();

            var jobSeekerData = labels.Select(date => jobSeekers.FirstOrDefault(js => js.Date == date)?.Count ?? 0).ToList();
            var employerData = labels.Select(date => employers.FirstOrDefault(em => em.Date == date)?.Count ?? 0).ToList();

            //Trả về JSON
            return Ok( new
                {
                    labels,
                    jobSeekerData,
                    employerData
                }
            );
        }

        [HttpPost("Statistical-Get-Job")]
        public async Task<IActionResult> GetJobStatistics(DateTime startDate, DateTime endDate)
        {
            // Giả sử bạn có repository để lấy dữ liệu người dùng
            var job = await _unitOfWork.StatisticalRepo.GetJobCountAndDateRange(startDate, endDate);

            // Xử lý dữ liệu theo từng ngày
            var labels = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                    .Select(offset => startDate.AddDays(offset).ToString("dd/MM/yyyy"))
                                    .ToList();

            var jobData = labels.Select(date => job.FirstOrDefault(js => js.Date == date)?.Count ?? 0).ToList();

            //Trả về JSON
            return Ok( new
                {
                    labels,
                    jobData
                }
            );
        }

        //job
        [HttpGet("Get-All-Job")]
        //[Authorize]
        public async Task<IActionResult> GetAllJob([FromQuery] JobQueryObject query)
        {
            var blog = await _unitOfWork.AccoutAdminRepo.GetAllJob(query);

            return Ok(blog);
        }

        [HttpPost("Update-Status-Job")]
        public IActionResult UpdateStatus(int Id,string Status)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>(Status);
            _unitOfWork.JobRepo.UpdateStatusJob(Id, status);
            return Ok("update successfully");
        }

        //Skill
        [HttpGet("Get-All-Skill")]
        //[Authorize]
        public async Task<IActionResult> GetAllSkill([FromQuery] SkillQuery query)
        {
            var skill = await _unitOfWork.AccoutAdminRepo.GetAllSkill(query);

            return Ok(skill);
        }

        [HttpPost("Add-Skill")]
        public IActionResult Create(CreateSkill skill)
        {
                Skill skillmodel= new Skill()
                {
                    Name = skill.Name,
                    IsDeleted= false
                };
                _unitOfWork.SkillRepo.Add(skillmodel);
                _unitOfWork.Save();
                return Ok("Add skill successfully");
        }


        [HttpPost("Update-Or-Delete-Skill")]
        public IActionResult Update(SkillDto skill)
        {
            Skill skillmodel = new Skill()
            {
                Id = skill.Id,
                Name = skill.Name,
                IsDeleted = skill.IsDelete
            };
            _unitOfWork.SkillRepo.Update(skillmodel);
            _unitOfWork.Save();
            return Ok("Update successfully");
        }
    }
}
