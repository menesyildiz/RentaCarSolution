using System.ComponentModel.DataAnnotations;

namespace RentaCar.Models
{
    public class MemberUpdateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
