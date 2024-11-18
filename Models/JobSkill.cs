using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Models
{
    public class JobSkill
    {
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        [ValidateNever]
        public Job Job { get; set; }

        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        [ValidateNever]
        public Skill Skill { get; set; }

    }
}
