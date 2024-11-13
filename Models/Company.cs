using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }


        public string Industry { get; set; }


        public string Description { get; set; }

        public string? Logo { get; set; }


        public string Website { get; set; }


        public string Email { get; set; }


        public string Phone { get; set; }

        public DateTime? Create { get; set; }


        public DateTime? Update { get; set; }


        public bool Status { get; set; }
    }
}
