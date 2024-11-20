using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Models;

namespace APIWeb1.Dtos.Job
{
    public class GetAllJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateOn { get; set; }
        public double Salary { get; set; }
        public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string JobStatus { get; set; }
        public GetEmployer Employer { get; set; }
        public string LocationShort { get; set; }
    }
}
