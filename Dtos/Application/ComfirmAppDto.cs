using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Application
{
    public class ComfirmAppDto
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
