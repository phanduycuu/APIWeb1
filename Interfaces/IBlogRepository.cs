using APIWeb1.Dtos.Blogs;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IBlogRepository
    {
        Task<Blog> CreateAsync(Blog blog);
        Task<List<GetAllBlogDto>> GetAllAsync(BlogQueryObject query);
        Task<List<GetAllBlogDto>> GetForEmployer(BlogQueryObject query,string userId);
        Task<List<Blog>> GetById(int blogId);
        Task<int> GetTotalAsync();
    }
}
