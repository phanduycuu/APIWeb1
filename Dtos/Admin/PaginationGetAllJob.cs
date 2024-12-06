using APIWeb1.Dtos.Blogs;
using APIWeb1.Dtos.Job;

namespace APIWeb1.Dtos.Admin
{
    public class PaginationGetAllJob
    {
        public List<GetAllJobDto> Jobs { get; set; }
        public int Total { get; set; }
    }
}
