using APIWeb1.Models;
using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.AppUsers
{
    public class UpdateUser
    {
        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(100, ErrorMessage = "Fullname must be between 1 and 100 characters.", MinimumLength = 1)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid birthdate format.")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(200, ErrorMessage = "Street must not exceed 200 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Ward is required.")]
        public string Ward { get; set; }

        [Required(ErrorMessage = "District is required.")]
        public string District { get; set; }
    }

}
