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

namespace Apartments_API.Controllers
{
    [Route("api/v{version:apiVersion}/ApartmentNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ApartmentNumberAPIV2Controller : ControllerBase
    {
        protected APIResponse _response;
        private readonly IApartmentNumberRepository _dbApartmentNumber;
        private readonly IApartmentRepository _dbApartment;
        private readonly IMapper _mapper;

        public ApartmentNumberAPIV2Controller(IApartmentNumberRepository dbApartmentNumber, IMapper mapper, IApartmentRepository dbApartment)
        {
            _dbApartmentNumber = dbApartmentNumber;
            _mapper = mapper;
            this._response = new();
            _dbApartment = dbApartment;
        }

        //[MapToApiVersion("2.0")]
        [HttpGet]

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
    }
}

