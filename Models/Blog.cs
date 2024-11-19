using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public AppUser User { get; set; }


        public string Img { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int Status { get; set; }
    }
}
