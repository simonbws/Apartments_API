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
        public ActionResult <IEnumerable<ApartmentDTO>> GetApartments()
        {
            return Ok(ApartmentStore.apartmentList);
            
        }
        [HttpGet("{id:int}")]
        public ActionResult <ApartmentDTO> GetApartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var apartment = ApartmentStore.apartmentList.FirstOrDefault(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return Ok(apartment);

        }
    }
}
