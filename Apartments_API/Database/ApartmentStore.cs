using Apartments_API.Models.DTO;

namespace Apartment_API.Database
{
    public class ApartmentStore
    {
        public static List<ApartmentDTO> apartmentList = new List<ApartmentDTO> {
            new ApartmentDTO{Id=1,Name="Pool View" },
            new ApartmentDTO{Id=2,Name="Beach View" }
            };
    }
}
