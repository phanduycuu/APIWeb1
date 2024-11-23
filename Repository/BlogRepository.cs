using APIWeb1.Data;
using APIWeb1.Dtos.Blogs;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIWeb1.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly ApplicationDBContext _context;
        public BlogRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Blog> CreateAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<List<BlogAdminDto>> GetAdminBlog()
        {
            return await _context.Blogs.Include(u=> u.User).
                Select(blog=> new BlogAdminDto
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Employername=blog.User.Fullname,
                    Status = blog.Status
                }).ToListAsync();
        }

        public async Task<List<GetAllBlogDto>> GetAllAsync(BlogQueryObject query)
        {
            var blog = _context.Blogs.Where(b => b.Status==1).Include(u=>u.User).ThenInclude(p=>p.Company).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                blog = blog.Where(s => s.Title.Contains(query.Title));
            }
            if (query.PageSize == 0 && query.PageNumber == 0)
                return await blog.Select(u => new GetAllBlogDto
                {
                    Id = u.Id,
                    Username = u.User.Fullname,
                    Companyname = u.User.Company.Name,
                    Img = u.Img,
                    Title = u.Title,
                    Content = u.Content
                }).ToListAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await blog.Skip(skipNumber).Take(query.PageSize).Select(u=> new GetAllBlogDto
            {
                Id = u.Id,
                Username=u.User.Fullname,
                Companyname=u.User.Company.Name,
                Img=u.Img,
                Title=u.Title,
                Content=u.Content
            }).ToListAsync();

        }


        public async Task<List<Blog>> GetById(int blogId)
        {
            var blog = _context.Blogs.Where(b => b.Id == blogId).Include(u => u.User);
            
            return await blog.ToListAsync();
        }

        public async Task<List<GetAllBlogDto>> GetForEmployer(BlogQueryObject query, string userId)
        {
            var blog = _context.Blogs.Where(b => b.UserId== userId ).Include(u => u.User).ThenInclude(p => p.Company).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                blog = blog.Where(s => s.Title.Contains(query.Title));
            }

            if (query.PageSize == 0 && query.PageNumber == 0)
                return await blog.Select(u => new GetAllBlogDto
                {
                    Id = u.Id,
                    Username = u.User.Fullname,
                    Companyname = u.User.Company.Name,
                    Img = u.Img,
                    Title = u.Title,
                    Content = u.Content
                }).ToListAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await blog.Skip(skipNumber).Take(query.PageSize).Select(u => new GetAllBlogDto
            {
                Id = u.Id,
                Username = u.User.Fullname,
                Companyname = u.User.Company.Name,
                Img = u.Img,
                Title = u.Title,
                Content = u.Content
            }).ToListAsync();
        }

        public async Task<int> GetTotalAsync(BlogQueryObject query, string userId)
        {
            var blog = _context.Blogs.Where(b => b.UserId == userId).Include(u => u.User).ThenInclude(p => p.Company).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                blog = blog.Where(s => s.Title.Contains(query.Title));
            }


            var totalblogs= blog.CountAsync();

            return await totalblogs;
        }

        public async Task<int> GetTotalWithConditions(BlogQueryObject query)
        {
            var blog = _context.Blogs.Where(b => b.Status == 1).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                blog = blog.Where(s => s.Title.Contains(query.Title));
            }


            var totalblogs = blog.CountAsync();

            return await totalblogs;
        }

        public void UpdateStatusBlog(int BlogId, int Status)
        {
            Blog? blog = _context.Blogs.Where(u => u.Id == BlogId).FirstOrDefault();
            blog.Status = Status;
            _context.Blogs.Update(blog);
            _context.SaveChanges();
        }
    }
}
