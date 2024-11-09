using APIWeb1.Dtos.Application;
using APIWeb1.Dtos.Company;
using APIWeb1.Models;

namespace APIWeb1.Mappers
{
    public static class ApplicationMapper
    {
        public static Application ToAppFromCreateDTO(this ApplicationDto appDto)
        {
            return new Application
            {
                JobId = appDto.JobId
            };
        }
    }
}
