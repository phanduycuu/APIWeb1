using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Blogs
{
    public class CreateBlogDto
    {
        [Required]
        public IFormFile Img { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

    }
}
