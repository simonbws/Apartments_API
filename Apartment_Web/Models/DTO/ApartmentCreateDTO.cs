using System.ComponentModel.DataAnnotations;

namespace Apartment_Web.Models.DTO
{
    public class ApartmentCreateDTO
    {
        [Required]
        [MaxLength(30)] // if is more than 30 char, it will show an error message
        public string Name { get; set; }
        public string? Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int SquareFeet { get; set; }
        public string? ImagePath { get; set; }
        public string? Amenity { get; set; }
        
    }
}
