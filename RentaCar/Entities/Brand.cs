using System.ComponentModel.DataAnnotations;

namespace RentaCar.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        public int AgeLimit { get; set; }

        public List<Modelx> Models { get; set; }

    }
}
