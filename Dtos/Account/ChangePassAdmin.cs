using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Account
{
    public class ChangePassAdmin
    {
        [Required]
        public string CurentPass { get; set; }
        [Required]
        public string NewPass { get; set; }
        [Required]
        public string RePass { get; set; }
    }
}
