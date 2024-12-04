using APIWeb1.Dtos.Companys;
using APIWeb1.Models;

namespace APIWeb1.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyDto ToCompanyDto(this Company companyModel)
        {
            return new CompanyDto
            {
                Id = companyModel.Id,
                Name = companyModel.Name,
                Industry = companyModel.Industry,
                Description = companyModel.Description,
                Logo = companyModel.Logo,
                Website = companyModel.Website,
                Email = companyModel.Email,
                Phone = companyModel.Phone,


            };
        }
        public static Company ToCompanyFromCreateDTO(this CreateCompanyRequestDto companyDto)
        {
            return new Company
            {
                Name = companyDto.Name,
                Industry = companyDto.Industry,
                Description = companyDto.Description,
                Logo = companyDto.Logo,
                Website = companyDto.Website,
                Email = companyDto.Email,
                Phone = companyDto.Phone,
                Status = true
            };
        }

        public static Company ToCompanyFromAdminAddCompany(this AdminAddCompany companyDto)
        {
            return new Company
            {
                Name = companyDto.Name,
                Industry = companyDto.Industry,
                Description = companyDto.Description,
                Website = companyDto.Website,
                Email = companyDto.Email,
                Phone = companyDto.Phone,
                Status = true
            };
        }
    }
}
