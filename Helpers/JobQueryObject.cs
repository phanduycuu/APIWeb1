using APIWeb1.Models.Enum;

namespace APIWeb1.Helpers
{
    public class JobQueryObject
    {
        public string? Title { get; set; } = null;
        public string? Location { get; set; } = null;
        public string? JobLevel { get; set; } = null;
        public string? JobType { get; set; } = null;
        public string? JobStatus { get; set; } = null;
        public string? SortBy { get; set; } = "CreateOn";
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;

    }
}
