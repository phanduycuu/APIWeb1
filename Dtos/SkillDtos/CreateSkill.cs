using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.SkillDtos
{
    public class CreateSkill
    {
        [Required]
        public string Name { get; set; }    
    }
}
