using APIWeb1.Dtos.Account;
using APIWeb1.Dtos.Addresses;
using APIWeb1.Dtos.Application;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Job;
using APIWeb1.Extensions;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/appuserjob")]
    [ApiController]
    public class AppUserJobController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AppUserJobController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;


        }
        
        [HttpGet("Get-User")]
        [Authorize]
        public async Task<IActionResult> GetAppUser()
        {
            var username = User.GetUsername();
            var userInfo = await _userManager.Users
                .Include(u => u.Company).Include(u => u.Address) // Load thông tin của Company
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (userInfo == null) 
            {
                return NotFound();
            }
            GetUserDto userDto = new GetUserDto()
            {
                Fullname = userInfo.Fullname,
                Username= userInfo.UserName,
                Email= userInfo.Email,
                Phone= userInfo.PhoneNumber,
                Company = userInfo.Company,
                Sex = userInfo.Sex,
                Birthdate = userInfo.Birthdate,
                Address = userInfo.Address
            };
            return Ok(userDto);
        }

        [HttpPost("Update-User")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser userdto)
        {
            var username = User.GetUsername();
            var userInfo = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.Fullname = userdto.Fullname;
            userInfo.PhoneNumber= userdto.Phone;
            userInfo.Email = userdto.Email;
            userInfo.Sex = userdto.Sex;
            userInfo.Birthdate = userdto.Birthdate;
            userInfo.AddressId = await CreateAddress(userdto.Street, userdto.Province, userdto.Ward, userdto.District);
            await _userManager.UpdateAsync(userInfo);
            return Ok("Update account successfully");
        }


        [HttpGet("employer-job")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetEmployerjob([FromQuery] JobQueryObject query)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userJob = await _unitOfWork.JobRepo.GetEmployerJob(appUser, query);
            return Ok(userJob);
        }
        
        [HttpGet("user-job")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserjob([FromQuery] JobQueryObject query)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userJob = await _unitOfWork.ApplicationRepo.GetUserJob(appUser, query);
            return Ok(userJob);
        }

        [HttpGet("user-issave-job")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserIsSavejob([FromQuery] JobQueryObject query)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userJob = await _unitOfWork.ApplicationRepo.GetAppUserIsSaveJob(appUser, query);
            return Ok(userJob);
        }

        //[HttpPost("create-job")]
        //[Authorize(Roles = "Employer")]
        //public async Task<IActionResult> Create([FromBody] CreateJobDto JobDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var username = User.GetUsername();
        //    var appUser = await _userManager.FindByNameAsync(username);
        //    var jobModel = JobDto.ToJobFromCreate(appUser.Id);
        //    jobModel.AddressId = await CreateAddress(JobDto.Street, JobDto.Province, JobDto.Ward, JobDto.District);
        //    await _unitOfWork.JobRepo.CreateAsync(jobModel);
        //    return Created();
        //}

        [HttpPost("create-job-skills")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto JobDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var jobModel = JobDto.ToJobFromCreate(appUser.Id);
            jobModel.AddressId = await CreateAddress(JobDto.Street, JobDto.Province, JobDto.Ward, JobDto.District);
            await _unitOfWork.JobRepo.CreateAsync(jobModel);
            if (JobDto.SkillIds != null && JobDto.SkillIds.Any())
            {
                var existingSkills = await _unitOfWork.JobSkillRepo.GetJobSkill(jobModel.Id);

                // Loại bỏ các skill đã tồn tại
                var newSkillIds = JobDto.SkillIds.Where(skillId => !existingSkills.Any(e => e.Id == skillId)).ToList();

                var jobSkills = newSkillIds.Select(skillId => new JobSkill
                {
                    JobId = jobModel.Id,
                    SkillId = skillId
                });

                foreach (var jobSkill in jobSkills)
                {
                    await _unitOfWork.JobSkillRepo.CreateAsync(jobSkill);
                }
                return Created();
            }

            return StatusCode(500, "Could not create job with skills");
        }

        [HttpPost("create-address")]
        public async Task<int> CreateAddress(string street, string province, string ward, string district)
        {
            Address addModel = new Address()
            {
                Street = street,
                Province = province,
                Ward = ward,
                District = district
            };
            await _unitOfWork.AddressRepo.CreateAsync(addModel);
            return addModel.Id;
        }

        //[HttpPost("create-application")]
        //[Authorize(Roles = "User")]
        //public async Task<IActionResult> CreateApplication(CreateApplicationDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var username = User.GetUsername();
        //    var appUser = await _userManager.FindByNameAsync(username);
        //    var filePath = "";

        //        filePath = Path.Combine(@"wwwroot\uploads\", $"{appUser.Id}_{dto.cvFile.FileName}_{dto.JobId}");

        //        // Lưu file vào hệ thống file
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await dto.cvFile.CopyToAsync(stream);
        //        }

        //    var app = await _unitOfWork.ApplicationRepo.GetAppUserJob(dto.JobId, appUser.Id);

        //    if (app != null)
        //    {
        //        app.Status = 1;
        //        app.Cv = filePath;
        //        app.Isshow = true;
        //        app.DateApply= DateTime.Now;
        //        await _unitOfWork.ApplicationRepo.UpdateAppUserJob(app);
        //        return Ok();

        //    }
        //    else
        //    {
        //        Application appModel = new Application();
        //        appModel.JobId = dto.JobId;
        //        appModel.UserId = appUser.Id;
        //        appModel.Status = 1;
        //        appModel.Isshow = true;
        //        appModel.Cv = filePath;
        //        app.DateApply = DateTime.Now;

        //        await _unitOfWork.ApplicationRepo.CreateAsync(appModel);
        //        return Created();
        //    }
        //}

        [HttpPost("create-application")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateApplication(CreateApplicationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var filePath = "";

            // Tạo tên file và đường dẫn
            var fileExtension = Path.GetExtension(dto.cvFile.FileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dto.cvFile.FileName);
            var fileName = $"{appUser.Id}_{fileNameWithoutExtension}_{dto.JobId}{fileExtension}";
            filePath = Path.Combine(@"wwwroot\uploads\", fileName);

            // Lưu file vào hệ thống
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.cvFile.CopyToAsync(stream);
            }

            var app = await _unitOfWork.ApplicationRepo.GetAppUserJob(dto.JobId, appUser.Id);

            if (app != null)
            {
                app.Status = 1;
                app.Cv = filePath;
                app.Isshow = true;
                app.DateApply = DateTime.Now;
                await _unitOfWork.ApplicationRepo.UpdateAppUserJob(app);
                return Ok();
            }
            else
            {
                Application appModel = new Application();
                appModel.JobId = dto.JobId;
                appModel.UserId = appUser.Id;
                appModel.Status = 1;
                appModel.Isshow = true;
                appModel.Cv = filePath;
                appModel.DateApply = DateTime.Now;

                await _unitOfWork.ApplicationRepo.CreateAsync(appModel);
                return Created();
            }
        }

        //[HttpGet("get-applications")]
        //[Authorize(Roles = "User")]
        //public async Task<IActionResult> GetApplicationsByUser()
        //{
        //    // Lấy tên người dùng từ token JWT
        //    var username = User.GetUsername();

        //    // Tìm người dùng trong hệ thống
        //    var appUser = await _userManager.FindByNameAsync(username);
        //    if (appUser == null)
        //    {
        //        return NotFound("User not found.");
        //    }

        //    // Lấy tất cả ứng tuyển của người dùng này
        //    var applications = await _unitOfWork.ApplicationRepo.GetApplicationsByUserId(appUser.Id);

        //    // Nếu không tìm thấy ứng tuyển nào
        //    if (applications == null || !applications.Any())
        //    {
        //        return NoContent(); // Hoặc trả về BadRequest nếu bạn muốn
        //    }

        //    // Trả về danh sách ứng tuyển (có thể dùng ApplicationDto hoặc mô hình khác)
        //    var applicationsDto = applications.Select(app => new ApplicationDto
        //    {
        //        Id = app.Id,
        //        JobId = app.JobId,
        //        Status = app.Status,
        //        Cv = app.Cv,
        //        DateApply = app.DateApply,
        //        JobTitle = app.Job.Title, // Giả sử bạn muốn trả về tiêu đề công việc
        //        EmployerName = app.Job.Employer.Name // Tên nhà tuyển dụng
        //    }).ToList();

        //    return Ok(applicationsDto);
        //}



        [HttpPost("issave")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateApplicatio(IsSaveApplication dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);           
            var app = await _unitOfWork.ApplicationRepo.GetAppUserJob(dto.JobId, appUser.Id);

            if (app != null)
            {
                app.Issave = dto.Issave;
                await _unitOfWork.ApplicationRepo.UpdateAppUserJob(app);
                return Ok();

            }
            else
            {
                Application appModel = new Application();
                appModel.JobId = dto.JobId;
                appModel.UserId = appUser.Id;
                appModel.Status = 0;
                appModel.Issave = true;

                await _unitOfWork.ApplicationRepo.CreateAsync(appModel);
                return Created();
            }
        }

        [HttpGet("employer-jobById")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetEmployerjobById(int JobId)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var Job = await _unitOfWork.JobRepo.GetJobById(JobId, appUser.Id);
            if (Job == null) 
                return NotFound("you don't have permition for this job or this job doesn't exist");
            return Ok(Job);
        }

        // get user by id
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            // Tìm kiếm người dùng trong cơ sở dữ liệu dựa trên userId
            var userInfo = await _userManager.Users
                //.Include(u => u.Company) // Bao gồm thông tin công ty
                .Include(u => u.Address) // Bao gồm thông tin địa chỉ
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());

            if (userInfo == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Chuyển đổi sang DTO để trả về client
            GetUserDto userDto = new GetUserDto()
            {
                Fullname = userInfo.Fullname,
                Username = userInfo.UserName,
                Email = userInfo.Email,
                Phone = userInfo.PhoneNumber,
                Company = userInfo.Company,
                Sex = userInfo.Sex,
                Birthdate = userInfo.Birthdate,
                Address = userInfo.Address
            };

            return Ok(userDto);
        }

        [HttpPost("Confirm-application")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> ConfirmApplication(ComfirmAppDto dto) // status= 2 duyet, status= 3 tu choi
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);           
            var app = await _unitOfWork.ApplicationRepo.GetEmployerApp(dto.JobId, dto.UserId, appUser.Id);

            if (app == null)
            {
                
                return BadRequest("You don't have permition for this job");

            }
            else
            {
                Application appModel = app;                
                appModel.Status = dto.Status;
                

                await _unitOfWork.ApplicationRepo.UpdateAppUserJob(appModel);
                return Ok("Update status successfully");
            }
        }
    }
}
