namespace APIWeb1.Helpers
{
    public class CompanyQueryObj
    {
        public string? Name { get; set; } = null;
        public string? Industry { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;


    }
}
