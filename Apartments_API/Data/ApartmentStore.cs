using Apartments_API.Models.DTO;

namespace Apartment_API.Data
{
    public class ApartmentStore
    {
        public static List<ApartmentDTO> apartmentList = new List<ApartmentDTO>
        {
            new ApartmentDTO{Id = 1, Name = "Swimming Pool Panorama"},
            new ApartmentDTO{Id = 2, Name = "Beach Panorama "}
        };
    }
}
