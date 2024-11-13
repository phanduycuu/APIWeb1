using APIWeb1.Dtos.Job;
using APIWeb1.Models;

namespace APIWeb1.Mappers
{
    public static class JobMapper
    {
        public static Job ToJobFromCreate(this CreateJobDto jobDto, string employerId)
        {
            return new Job
            {
                Title = jobDto.Title,
                Description = jobDto.Description,
                Requirements = jobDto.Requirements,
                Benefits = jobDto.Benefits,
                Salary = jobDto.Salary,
                ExpiredDate = jobDto.ExpiredDate,
                CreateOn = jobDto.CreateOn,
                UpdatedOn = jobDto.UpdatedOn,
                EmployerId = employerId,
                JobLevel = jobDto.JobLevel,
                JobType = jobDto.JobType,
                JobStatus = jobDto.JobStatus,
                AddressId = jobDto.Location
            };
        }

       
    }
}
