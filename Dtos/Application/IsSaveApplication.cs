using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Application
{
    public class IsSaveApplication
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        public bool Issave { get; set; }
    }
}
