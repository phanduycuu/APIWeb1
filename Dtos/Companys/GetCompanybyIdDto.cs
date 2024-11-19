using APIWeb1.Dtos.Job;

namespace APIWeb1.Dtos.Companys
{
    public class GetCompanybyIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Create { get; set; }
        public DateTime? Update { get; set; }
        public bool Status { get; set; }
        public List<JobDto> Jobs { get; set; } = new List<JobDto>();
    }
}
