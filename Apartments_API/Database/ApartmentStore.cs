using Apartments_API.Models.DTO;

namespace Apartment_API.Data
{
    public class ApartmentStore
    {
        public static List<ApartmentDTO> apartmentList = new List<ApartmentDTO>
        {
            new ApartmentDTO{Id = 1, Name = "Swimming Pool Panorama", SquareFeet = 120, Occupancy = 4},
            new ApartmentDTO{Id = 2, Name = "Beach Panorama ", SquareFeet = 80, Occupancy = 4}
        };
    }
}
