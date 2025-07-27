

namespace APIWeb1.Dtos.AppUsers
{
    public class EmployerDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public APIWeb1.Models.Company Company { get; set; }
    }
}
