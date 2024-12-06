using APIWeb1.Models;

namespace APIWeb1.Dtos.AppUsers
{
    public class AdminAccountUser
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string? Img { get; set; }
        public string? Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Companyname { get; set; }
        public string? Sex { get; set; }
        public DateTime? Birthdate { get; set; }
        public Address? Address { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
    }
}
