using APIWeb1.Data;
using APIWeb1.Dtos.Companys;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.ApiControllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDBContext _context;
        public CompanyController(ApplicationDBContext context, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        
        [HttpGet("GetAllForEmployer")]
        public async Task<IActionResult> GetAllForEmployer()
        {
            
            var companies = await _unitOfWork.CompanyRepo.GetAllAsync();
            var companyDtos = companies.Select(c => c.ToCompanyDto()).ToList();
            return Ok(companyDtos);
        }

        [HttpGet("GetAllForUser")]
        public async Task<IActionResult> GetAllForUser([FromQuery] CompanyQueryObj obj)
        {

            var companies = await _unitOfWork.CompanyRepo.GetAllAsyncForUser(obj);
            var companyDtos = companies.Select(c => c.ToCompanyDto()).ToList();
            return Ok(companyDtos);
        }
    }
}
