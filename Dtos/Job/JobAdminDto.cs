using APIWeb1.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Job
{
    public class JobAdminDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EmployerName { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string JobStatus { get; set; }
    }
}
