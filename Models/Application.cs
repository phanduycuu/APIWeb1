using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Models
{
    public class Application
    {
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        [ValidateNever]
        public Job Job { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public AppUser User { get; set; }
        public DateTime? DateApply { get; set; }

        public string Cv { get; set; }
        public int Status { get; set; }
        public bool IsSale  { get; set; }

    }
}
