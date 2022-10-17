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

namespace Apartment_API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/ApartmentNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
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

        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


    }
}

