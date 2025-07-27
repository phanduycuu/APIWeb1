using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.AppUsers
{
    public class Adminprofile
    {
        public string Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        public string Email { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public DateTime? Birthdate { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        
    }
}
