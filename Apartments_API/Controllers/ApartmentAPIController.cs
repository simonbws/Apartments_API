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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult <IEnumerable<ApartmentDTO>> GetApartments()
        {
            return Ok(ApartmentStore.apartmentList);
            
        }
        [HttpGet("{id:int}", Name ="GetApartment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        public ActionResult<ApartmentDTO> CreateApartment(ApartmentDTO apartmentDTO)
        {
            if (apartmentDTO == null)
            {
                return BadRequest(apartmentDTO);
            }
            if (apartmentDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            apartmentDTO.Id = ApartmentStore.apartmentList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            ApartmentStore.apartmentList.Add(apartmentDTO); //we added this villa object to Store

            return CreatedAtRoute("GetApartment", new { id = apartmentDTO.Id },apartmentDTO);
        }
    }
}
