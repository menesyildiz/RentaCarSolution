using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace RentaCar.Entities
{
    public class Modelx
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Details { get; set; }

        public Brand Brand { get; set; }
        
        public int BrandId { get; set; } // brand class ın ıd'sini anlıyor.
    }
}
