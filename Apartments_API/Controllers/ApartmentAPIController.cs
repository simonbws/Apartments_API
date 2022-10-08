using Apartment_API.Database;
using Apartments_API.Models;
using Apartments_API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Apartments_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ApartmentAPIController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult <IEnumerable<ApartmentDTO>> GetApartments()
        {
            
            return Ok(_db.Apartments.ToList());
            
        }
        [HttpGet("{id:int}", Name ="GetApartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <ApartmentDTO> GetApartment(int id)
        {
            if (id == 0)
            {
                
                return BadRequest();
            }
            var apartment = _db.Apartments.FirstOrDefault(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return Ok(apartment);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApartmentDTO> CreateApartment([FromBody]ApartmentDTO apartmentDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (_db.Apartments.FirstOrDefault(u => u.Name.ToLower() == apartmentDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Apartment already exist!");
                return BadRequest(ModelState);
            }
            if (apartmentDTO == null)
            {
                return BadRequest(apartmentDTO);
            }
            if (apartmentDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Apartment model = new()
            {
                Amenity = apartmentDTO.Amenity,
                Details = apartmentDTO.Details,
                Id = apartmentDTO.Id,
                ImagePath = apartmentDTO.ImagePath,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                Rate = apartmentDTO.Rate,
                SquareFeet = apartmentDTO.SquareFeet
            };
            _db.Apartments.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetApartment", new { id = apartmentDTO.Id },apartmentDTO);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteApartment")]

        public IActionResult DeleteApartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //if is not equal to zero, then from the villa store, we will use first or default
            //and retrieve villa that we have to deleete
            var apartment = _db.Apartments.FirstOrDefault(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            //if we do find apartment, then we will use apartmentstore, remove and we will
            //pass apartment which we want to remove
            _db.Apartments.Remove(apartment);
            _db.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateApartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]     
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //we need to pass int i, we want to update, and complete object from body ( apartmentdto)
        public IActionResult UpdateApartment(int id, [FromBody]ApartmentDTO apartmentDTO)
        {
            //we want to check if aprtm is null or id which we pass from method IActionResult
            //is not the same with that which we pass from the object apartmentDTO
            if(apartmentDTO == null || id !=apartmentDTO.Id)
            {
                return BadRequest();
            }
            //we need to retrieve based on the id which we passed into method
            //var apartment = ApartmentStore.apartmentList.FirstOrDefault(u=>u.Id == id);
            ////now we update square feet and occupancy based on the apartmentDTO
            //apartment.Name = apartmentDTO.Name;
            //apartment.SquareFeet = apartmentDTO.SquareFeet;
            //apartment.Occupancy = apartmentDTO.Occupancy;
            //we need to convert apartmentDTO to apartment object
            Apartment model = new()
            {
                Amenity = apartmentDTO.Amenity,
                Details = apartmentDTO.Details,
                Id = apartmentDTO.Id,
                ImagePath = apartmentDTO.ImagePath,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                Rate = apartmentDTO.Rate,
                SquareFeet = apartmentDTO.SquareFeet
            };
            _db.Apartments.Update(model);
            _db.SaveChanges();
            return NoContent();

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialApartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialApartment(int id, JsonPatchDocument<ApartmentDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            //if id is not zero, we can try to retrieve apartment from apartment list
            var apartment = _db.Apartments.FirstOrDefault(u=>u.Id==id);
            //type is apartmentDTO so we have to convert
            ApartmentDTO apartmentDTO = new()
            {
                Amenity = apartment.Amenity,
                Details = apartment.Details,
                Id = apartment.Id,
                ImagePath = apartment.ImagePath,
                Name = apartment.Name,
                Occupancy = apartment.Occupancy,
                Rate = apartment.Rate,
                SquareFeet = apartment.SquareFeet
            };

            if (apartment == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(apartmentDTO, ModelState);
            Apartment model = new Apartment()
            {
                Amenity = apartmentDTO.Amenity,
                Details = apartmentDTO.Details,
                Id = apartmentDTO.Id,
                ImagePath = apartmentDTO.ImagePath,
                Name = apartmentDTO.Name,
                Occupancy = apartmentDTO.Occupancy,
                Rate = apartmentDTO.Rate,
                SquareFeet = apartmentDTO.SquareFeet
            };

            _db.Apartments.Update(model);
            _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }
}
