using System.ComponentModel.DataAnnotations;

namespace RentaCar.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Confirmed { get; set; }
        public string Role { get; set; }
    }
}
