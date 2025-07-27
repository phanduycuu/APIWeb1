using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Companys
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Industry { get; set; }


        public string Description { get; set; }


        public string? Logo { get; set; }


        public string Website { get; set; }


        public string Email { get; set; }


        public string Phone { get; set; }

    }
}
