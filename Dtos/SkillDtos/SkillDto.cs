using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace APIWeb1.Dtos.SkillDtos
{
    public class SkillDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsDelete { get; set; }

    }
}
