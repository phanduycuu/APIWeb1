using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Account
{
    public class ChangePass
    {
        [Required]
        public string CurentPass { get; set; }
        [Required]
        public string NewPass { get; set; }
    }
}
