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
using System.Data;
using System.Net;

namespace Apartment_API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/ApartmentNumberAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class ApartmentNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IApartmentNumberRepository _dbApartmentNumber;
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;

        public ApartmentNumberAPIController(IApartmentNumberRepository dbApartmentNumber, IMapper mapper, IApartmentRepository dbApartment)
        {
            _dbApartmentNumber = dbApartmentNumber;
            _mapper = mapper;
            _response = new();
            _dbApartment = dbApartment;
        }

        [HttpGet("GetString")]

        public IEnumerable<string> Get()
        {
            return new string[] { "String1", "String2" };
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetApartmentNumbers()
        {
            try
            {
                IEnumerable<ApartmentNumber> apartmentNumberList = await _dbApartmentNumber.GetAllAsync(includeProperties: "Apartment");
                _response.Result = _mapper.Map<List<ApartmentNumberDTO>>(apartmentNumberList);
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

        [HttpGet("{id:int}", Name = "GetApartmentNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetApartmentNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var apartmentNumber = await _dbApartmentNumber.GetAsync(u => u.ApartmentNo == id);
                if (apartmentNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ApartmentNumberDTO>(apartmentNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateApartmentNumber([FromBody] ApartmentNumberCreateDTO createDTO)
        {
            try
            {

                if (await _dbApartmentNumber.GetAsync(u => u.ApartmentNo == createDTO.ApartmentNo) != null)
                {
                    ModelState.AddModelError("Errors", "Apartment Number already exist!");
                    return BadRequest(ModelState);
                }
                if (await _dbApartment.GetAsync(u => u.Id == createDTO.ApartmentID) == null)
                {
                    ModelState.AddModelError("Errors", "Apartment ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                ApartmentNumber apartmentNumber = _mapper.Map<ApartmentNumber>(createDTO);

                await _dbApartmentNumber.CreateAsync(apartmentNumber);
                _response.Result = _mapper.Map<ApartmentNumberDTO>(apartmentNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetApartment", new { id = apartmentNumber.ApartmentNo }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Errors
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteApartmentNumber")]

        public async Task<ActionResult<APIResponse>> DeleteApartmentNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var apartmentNumber = await _dbApartmentNumber.GetAsync(u => u.ApartmentNo == id);
                if (apartmentNumber == null)
                {
                    return NotFound();
                }
                await _dbApartmentNumber.RemoveAsync(apartmentNumber);
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
            return _response;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateApartmentNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateApartmentNumber(int id, [FromBody] ApartmentNumberUpdateDTO updateDTO)
        {
            try
            {

                if (updateDTO == null || id != updateDTO.ApartmentNo)
                {
                    return BadRequest();
                }
                if (await _dbApartment.GetAsync(u => u.Id == updateDTO.ApartmentID) == null)
                {
                    ModelState.AddModelError("Errors", "Apartment ID is Invalid!");
                    return BadRequest(ModelState);
                }

                ApartmentNumber model = _mapper.Map<ApartmentNumber>(updateDTO);

                await _dbApartmentNumber.UpdateAsync(model);
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
            return _response;
        }
    }
}

