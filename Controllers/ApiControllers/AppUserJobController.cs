using APIWeb1.Dtos.Application;
using APIWeb1.Dtos.Job;
using APIWeb1.Extensions;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            await _unitOfWork.JobRepo.CreateAsync(jobModel);
            return Created();
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
