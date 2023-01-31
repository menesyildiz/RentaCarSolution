using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}
