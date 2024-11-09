using APIWeb1.Dtos.AppUsers;
using APIWeb1.Models;

namespace APIWeb1.Dtos.Job
{
    public class GetJobByIdDto
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
        
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string JobStatus { get; set; }
        public string Location { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<AppUserDto> Users { get; set; } = new List<AppUserDto>();
    }
}
