using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Companys
{
    public class AdminAddCompany
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        [Required]
        public IFormFile Img { get; set; }

    }
}
