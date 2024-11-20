using APIWeb1.Models.Enum;
using APIWeb1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Dtos.AppUsers;

namespace APIWeb1.Dtos.Job
{
    public class JobDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public string Benefits { get; set; }

        public double Salary { get; set; }

        public DateTime? ExpiredDate { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public GetEmployerDto Employer { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string JobStatus { get; set; }
        public string Location { get; set;}
        public string LocationShort { get; set;}
        public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
    }
}
