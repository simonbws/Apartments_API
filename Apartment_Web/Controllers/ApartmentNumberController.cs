using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services;
using Apartment_Web.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Apartment_Web.Controllers
{
    public class ApartmentNumberController : Controller
    {
        private readonly IApartmentNumberService _apartmentNumberService;
        private readonly IMapper _mapper;

        public ApartmentNumberController(IApartmentNumberService apartmentNumberService, IMapper mapper)
        {
            _apartmentNumberService = apartmentNumberService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexApartmentNumber()
        {
            List<ApartmentNumberDTO> list = new();

            var response = await _apartmentNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.isSuccess)
            {
                //we convert that to string, then we deserialize that to list od apart dto and we assign that to list
                //result will be ApartmentNumberDTO
                list = JsonConvert.DeserializeObject<List<ApartmentNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);

        }
    }
}
