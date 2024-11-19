using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string? Img { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company { get; set; }
        public string? Sex { get; set; }
        public DateTime? Birthdate { get; set; }
        public int Status { get; set; }
        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        [ValidateNever]
        public Address? Address { get; set; }
        public List<Application> Applications { get; set; } = new List<Application>();
        public List<Job> Jobs { get; set; } = new List<Job>();


    }
}
