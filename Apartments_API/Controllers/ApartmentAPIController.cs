using Apartment_API.Database;
using Apartment_API.Repository.IRepository;
using Apartments_API.Models;
using Apartments_API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Apartments_API.Controllers
{
    [Route("api/ApartmentAPI")]
    [ApiController]
    public class ApartmentAPIController : ControllerBase
    {
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;

        public ApartmentAPIController(IApartmentRepository dbApartment, IMapper mapper)
        {

            _dbApartment = dbApartment;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult <IEnumerable<ApartmentDTO>>> GetApartments()
        {
            
            IEnumerable<Apartment> apartmentList = await _dbApartment.GetAllAsync();
            //we need to now convert that to villa dto, destination type is apartDTO and in
            //parameter we need to pass object which is apartmentList
            return Ok(_mapper.Map<List<ApartmentDTO>>(apartmentList));
            
        }
        [HttpGet("{id:int}", Name ="GetApartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult <ApartmentDTO>> GetApartment(int id)
        {
            if (id == 0)
            {
                
                return BadRequest();
            }
            var apartment = await _dbApartment.GetAsync(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ApartmentDTO>(apartment));

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApartmentDTO>> CreateApartment([FromBody]ApartmentCreateDTO createDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (await _dbApartment.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Apartment already exist!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            //if (apartmentDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //now we need to do conversion, output is Apartment, input createDTO
            Apartment model = _mapper.Map<Apartment>(createDTO);
            //that will do the mapping, we do not need to this properties below (commented)
            //Apartment model = new()
            //{
            //    Amenity = createDTO.Amenity,
            //    Details = createDTO.Details,       
            //    ImagePath = createDTO.ImagePath,
            //    Name = createDTO.Name,
            //    Occupancy = createDTO.Occupancy,
            //    Rate = createDTO.Rate,
            //    SquareFeet = createDTO.SquareFeet
            //};
            await _dbApartment.CreateAsync(model);
            return CreatedAtRoute("GetApartment", new { id = model.Id },model);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteApartment")]

        public async Task<IActionResult> DeleteApartment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            //if is not equal to zero, then from the villa store, we will use first or default
            //and retrieve villa that we have to deleete
            var apartment = await _dbApartment.GetAsync(u => u.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            //if we do find apartment, then we will use apartmentstore, remove and we will
            //pass apartment which we want to remove
            await _dbApartment.RemoveAsync(apartment);         
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateApartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]     
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //we need to pass int i, we want to update, and complete object from body ( apartmentdto)
        public async Task<IActionResult> UpdateApartment(int id, [FromBody]ApartmentUpdateDTO updateDTO)
        {
            //we want to check if aprtm is null or id which we pass from method IActionResult
            //is not the same with that which we pass from the object apartmentDTO
            if(updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }
            
            Apartment model = _mapper.Map<Apartment>(updateDTO);

            await _dbApartment.UpdateAsync(model);      
            return NoContent();

        }
        [HttpPatch("{id:int}", Name = "UpdatePartialApartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialApartment(int id, JsonPatchDocument<ApartmentUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            //if id is not zero, we can try to retrieve apartment from apartment list
            var apartment = await _dbApartment.GetAsync(u=>u.Id==id, tracked:false);

            //type is apartmentDTO so we have to convert
            ApartmentUpdateDTO apartmentDTO = _mapper.Map<ApartmentUpdateDTO>(apartment);
           

            if (apartment == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(apartmentDTO, ModelState);
            Apartment model = _mapper.Map<Apartment>(apartmentDTO);

            await _dbApartment.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }
}
