using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string Province { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }

    }
}
