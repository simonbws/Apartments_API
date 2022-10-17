using Apartment_API.Database;
using Apartment_API.Models;
using Apartment_API.Repository.IRepository;
using Apartments_API.Models;
using Apartments_API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace Apartment_API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/ApartmentAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ApartmentAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;

        public ApartmentAPIController(IApartmentRepository dbApartment, IMapper mapper)
        {

            _dbApartment = dbApartment;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        // only authorize user is able to access this endpoint
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetApartments()
        {
            try
            {
                IEnumerable<Apartment> apartmentList = await _dbApartment.GetAllAsync();
                _response.Result = _mapper.Map<List<ApartmentDTO>>(apartmentList);
                _response.StatusCode = HttpStatusCode.OK;
                //we need to now convert that to villa dto, destination type is apartDTO and in
                //parameter we need to pass object which is apartmentList
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors
                    = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetApartment")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApartment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var apartment = await _dbApartment.GetAsync(u => u.Id == id);
                if (apartment == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ApartmentDTO>(apartment);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateApartment([FromBody] ApartmentCreateDTO createDTO)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (await _dbApartment.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Errors", "Apartment already exist!");
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
                Apartment apartment = _mapper.Map<Apartment>(createDTO);
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
                await _dbApartment.CreateAsync(apartment);
                _response.Result = _mapper.Map<ApartmentDTO>(apartment);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetApartment", new { id = apartment.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors
                    = new List<string>() { ex.ToString() };
            }
            return _response;  //_response is returned model
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteApartment")]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<APIResponse>> DeleteApartment(int id)
        {
            try
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
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors
                    = new List<string>() { ex.ToString() };
            }
            return _response;  //_response is returned model
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateApartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //we need to pass int i, we want to update, and complete object from body ( apartmentdto)
        public async Task<ActionResult<APIResponse>> UpdateApartment(int id, [FromBody] ApartmentUpdateDTO updateDTO)
        {
            try
            {
                //we want to check if aprtm is null or id which we pass from method IActionResult
                //is not the same with that which we pass from the object apartmentDTO
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Apartment model = _mapper.Map<Apartment>(updateDTO);

                await _dbApartment.UpdateAsync(model); // we update everything here
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return Ok(_response); // we return response here
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors
                    = new List<string>() { ex.ToString() };
            }
            return _response;  //_response is returned model
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
            var apartment = await _dbApartment.GetAsync(u => u.Id == id, tracked: false);

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
