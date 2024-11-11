using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }

        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company { get; set; }
        public int Status { get; set; }
        public List<Application> Applications { get; set; } = new List<Application>();

    }
}
