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

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var companies = await _unitOfWork.CompanyRepo.GetAllAsync();
            var companyDtos = companies.Select(c => c.ToCompanyDto()).ToList();
            return Ok(companyDtos);
        }
    }
}
