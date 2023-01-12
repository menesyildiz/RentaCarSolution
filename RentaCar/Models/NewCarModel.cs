using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class NewCarModel // Sayfamızın modelinin adı
    {
        [StringLength(50)]
        public string Brand { get; set; }
        [StringLength(50)]
        public string Model { get; set; }
        [StringLength(10)]
        public string Year { get; set; }
        [StringLength(20)]
        public string GearBox { get; set; }
        [StringLength(20)]
        public string Fuel { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
