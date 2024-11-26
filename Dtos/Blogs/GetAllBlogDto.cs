using APIWeb1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Blogs
{
    public class GetAllBlogDto
    {

        public int Id { get; set; }

        public string Username { get; set; }
        public string Companyname { get; set; }

        public string Img { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? CreateOn { get; set; }
        public bool IsShow { get; set; }
    }
}
