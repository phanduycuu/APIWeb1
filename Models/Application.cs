using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        [ValidateNever]
        public Job Job { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public AppUser User { get; set; }
        public DateTime? DateApply { get; set; }

        public string? Cv { get; set; }
        public int Status { get; set; }  //0 chua gui,1 dang duyet,2 da duyet,3 tu choi
        public bool IsSale  { get; set; } 

    }
}
