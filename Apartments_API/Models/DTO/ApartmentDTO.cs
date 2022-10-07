using System.ComponentModel.DataAnnotations;

namespace Apartments_API.Models.DTO
{
    public class ApartmentDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)] // if is more than 30 char, it will show an error message
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int SquareFeet { get; set; }
    }
}
