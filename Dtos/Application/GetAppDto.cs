using APIWeb1.Models;

namespace APIWeb1.Dtos.Application
{
    public class GetAppDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public string Benefits { get; set; }

        public double Salary { get; set; }

        public DateTime? ExpiredDate { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public AppUser Employer { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string JobStatus { get; set; }
        public string Location { get; set; }
        public string CV { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}
