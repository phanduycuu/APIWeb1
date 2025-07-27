using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Blogs;

namespace APIWeb1.Dtos.Admin
{
    public class PaginationGetAllBlog
    {
        public List<GetAllBlogDto> Blogs { get; set; }
        public int Total { get; set; }
    }
}

