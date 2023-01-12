using RentaCar.Entities;

namespace RentaCar.Models
{
    public class RentViewModel
    {
        public DateTime? DeliveryDate { get; set; }

        public Car Car { get; set; }
    }
}
