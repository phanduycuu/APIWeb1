using APIWeb1.Data;
using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/skill")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDBContext _context;
        public SkillController(ApplicationDBContext context, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var skills = await _unitOfWork.SkillRepo.GetAllAsync();
            var skillDtos = skills.Select(c => c.ToSkillDto()).ToList();
            return Ok(skillDtos);
        }
    }
}