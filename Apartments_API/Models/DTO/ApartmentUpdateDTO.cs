using System.ComponentModel.DataAnnotations;

namespace Apartments_API.Models.DTO
{
    public class ApartmentUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)] // if is more than 30 char, it will show an error message
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int SquareFeet { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public string Amenity { get; set; }
        
    }
}
