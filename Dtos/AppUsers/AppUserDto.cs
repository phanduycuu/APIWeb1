namespace APIWeb1.Dtos.AppUsers
{
    public class AppUserDto
    {
        public string Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CV { get; set; }

        public int Status { get; set; }
    }
}
