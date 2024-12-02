using APIWeb1.Dtos.Blogs;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;
using APIWeb1.Models.Enum;

namespace APIWeb1.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<Blog> CreateAsync(Blog blog);
        Task<List<GetAllBlogDto>> GetAllAsync(BlogQueryObject query);
        Task<List<GetAllBlogDto>> GetForEmployer(BlogQueryObject query,string userId);
        Task<Blog> GetByIdForAll(int blogId);
        Task<int> GetTotalAsync(BlogQueryObject query, string userId);
        Task<int> GetTotalWithConditions(BlogQueryObject query);
        void UpdateStatusBlog(int BlogId, int Status);
        Task<List<BlogAdminDto>> GetAdminBlog();
        Task<Blog> GetByIdForEmployer(int blogId, string EmployerId);
        Task<Blog> UpdateEmployerblog(Blog blog);
        Task Deleteblog(Blog blog);
    }
}
