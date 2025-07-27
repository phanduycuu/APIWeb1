using APIWeb1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb1.Dtos.AppUsers
{
    public class GetUserDto
    {
        public string Fullname { get; set; }
        public string? Img { get; set; }
        public string? Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Company? Company { get; set; }
        public string? Sex { get; set; }
        public DateTime? Birthdate { get; set; }
        public Address? Address { get; set; }
    }
}
