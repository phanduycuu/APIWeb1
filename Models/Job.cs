using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APIWeb1.Models.Enum;

namespace APIWeb1.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string Benefits { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public DateTime? ExpiredDate { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string EmployerId { get; set; }
        [ForeignKey("EmployerId")]
        [ValidateNever]
        public AppUser Employer { get; set; }
        public JobLevel JobLevel { get; set; }
        public JobType JobType { get; set; }
        public JobStatus JobStatus { get; set; }
        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        [ValidateNever]
        public Address? Address { get; set; }
        public bool IsNew { get; set; }
        public bool IsFetured { get; set; }
        public bool IsShow{ get; set; }

        public List<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Application> Applications { get; set; } = new List<Application>();


    }
}
