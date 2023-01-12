using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace RentaCar.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(11)]
        public string IdNumber { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

    }
}
