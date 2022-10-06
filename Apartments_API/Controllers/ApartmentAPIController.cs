using Apartment_API.Data;
using Apartments_API.Models;
using Apartments_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Apartments_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ApartmentDTO> GetApartments()
        {
            return ApartmentStore.apartmentList;
            
        }
        [HttpGet("{id:int}")]
        public ApartmentDTO GetApartment(int id)
        {
            return ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);

        }
    }
}
