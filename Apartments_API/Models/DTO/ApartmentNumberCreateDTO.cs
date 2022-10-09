using System.ComponentModel.DataAnnotations;

namespace Apartments_API.Models.DTO
{
    public class ApartmentNumberCreateDTO
    {
        [Required]
        public int ApartmentNo { get; set; }
        public string SpecialProperties { get; set; }

    }
}
