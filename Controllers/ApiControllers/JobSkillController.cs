using APIWeb1.Extensions;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/jobskill")]
    [ApiController]
    public class JobSkillController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public JobSkillController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)

        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddJobSkill(int skillId,int jobId)
        {
            

            var Skill = await _unitOfWork.JobSkillRepo.GetJobSkill(jobId);

            if (Skill.Any(e => e.Id == skillId)) return BadRequest("Cannot add same Skill to Job");

            var JobSkill = new JobSkill
            {
                SkillId = skillId,
                JobId = jobId
            };

            await _unitOfWork.JobSkillRepo.CreateAsync(JobSkill);

            if (JobSkill == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
    }
}
