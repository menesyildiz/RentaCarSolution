using RentaCar.Entities;
using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class EditBrandViewModel
    {
        [StringLength(50)]
        public string Name { get; set; }
        public int AgeLimit { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
