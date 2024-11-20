using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Application
{
    public class CreateApplicationDto
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public IFormFile cvFile { get; set; }
    }
}
