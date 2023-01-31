using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [MinLength(6), MaxLength(16)]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
