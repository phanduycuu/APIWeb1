using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Account
{
    public class RegisterEmployerDto
    {
        [Required]
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
