namespace APIWeb1.Dtos.Job
{
    public class AdminGetJob
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string JobLevel { get; set; }
        public string JobType { get; set; }
        public string JobStatus { get; set; }
        public string LocationShort { get; set; }
        public string Employername { get; set; }
        public string Companyname { get; set; }
    }
}
