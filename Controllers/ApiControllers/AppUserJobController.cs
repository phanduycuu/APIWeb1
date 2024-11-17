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

        [HttpPost("create-job")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Create([FromBody] CreateJobDto JobDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var jobModel = JobDto.ToJobFromCreate(appUser.Id);
            jobModel.AddressId = await CreateAddress(JobDto.Street, JobDto.Province, JobDto.Ward, JobDto.District);
            await _unitOfWork.JobRepo.CreateAsync(jobModel);
            return Created();
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

        [HttpPost("create-application")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateApplication(int JobId,int status,  IFormFile? cvFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var filePath = "";
            if (cvFile != null)
            {
                filePath = Path.Combine("wwwroot/uploads", $"{appUser.Id}_{cvFile.FileName}");

                // Lưu file vào hệ thống file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }
            }
            var app = await _unitOfWork.ApplicationRepo.GetAppUserJob(JobId, appUser.Id);

            if (app != null)
            {
                app.Status = status;
                app.Cv = filePath;
                await _unitOfWork.ApplicationRepo.UpdateAppUserJob(app);
                return Ok();

            }
            else
            {
                Application appModel = new Application();
                appModel.JobId = JobId;
                appModel.UserId = appUser.Id;
                appModel.Status = status;
                appModel.IsSale = true;
                appModel.Cv = filePath;

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
            var Job = await _unitOfWork.JobRepo.GetJobById(JobId);
            return Ok(Job);
        }
    }
}
