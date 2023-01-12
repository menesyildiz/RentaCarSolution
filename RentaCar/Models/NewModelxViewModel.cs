using Microsoft.AspNetCore.Mvc.Rendering;
using RentaCar.Entities;
using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class NewModelxViewModel
    {
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Details { get; set; }

        public int BrandId { get; set; }

        public SelectList? Brands { get; set; }
    }
}
