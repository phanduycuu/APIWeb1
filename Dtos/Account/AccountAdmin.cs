using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Account
{
    public class AccountAdmin
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? CompanyName { get; set; }
        public int Status { get; set; }
    }
}
