using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APIWeb1.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Skill name is required")]
        [DisplayName("Skill Name")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    }
}
