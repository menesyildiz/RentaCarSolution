using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class NewCustomerModel
    {
        [MaxLength(11)]
        [Required]
        public string IdNumber { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        [StringLength(11)]
        [Required]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
