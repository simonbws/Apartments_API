using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Apartment_Web.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }
 
        public async Task<IActionResult> IndexApartment()
        {
            List<ApartmentDTO> list = new();

            var response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.isSuccess)
            {
                //we convert that to string, then we deserialize that to list od apart dto and we assign that to list
                list = JsonConvert.DeserializeObject<List<ApartmentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
            
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> CreateApartment()
        {
            
            return View();

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApartment(ApartmentCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _apartmentService.CreateAsync<APIResponse>(model);
                if (response != null && response.isSuccess)

                {
                    TempData["success"] = "Apartment has been created";
                    return RedirectToAction(nameof(IndexApartment));
                }
            }
            TempData["ERROR"] = "Error. Please check if everything is okay.";
            return View(model);

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateApartment(int apartmentId) // we will get aprt id
        {
            //then we will pass that id
            var response = await _apartmentService.GetAsync<APIResponse>(apartmentId);
            //and we will retrieve the complete apartment
            if (response != null && response.isSuccess)
            {
                
                //we will desiarlize that to convert to string first
                ApartmentDTO model = JsonConvert.DeserializeObject<ApartmentDTO>(Convert.ToString(response.Result));
                //before we return back to the view, we can convert that using automapper to apartmentupdateDTO
                return View(_mapper.Map<ApartmentUpdateDTO>(model));
                //if the response is null we return notfound
            }
            return NotFound();


        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApartment(ApartmentUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Apartment has been updated";
                var response = await _apartmentService.UpdateAsync<APIResponse>(model);
                if (response != null && response.isSuccess)
                {
                    //we redirect back to index apartment
                    return RedirectToAction(nameof(IndexApartment));
                }
                
            }
            TempData["ERROR"] = "Error. Please check if everything is okay.";
            //else we return back if the model state is not valid
            return View(model);

        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteApartment(int apartmentId) // we will get aprt id
        {
            //then we will pass that id
            var response = await _apartmentService.GetAsync<APIResponse>(apartmentId);
            //and we will retrieve the complete apartment
            if (response != null && response.isSuccess)
            {
                //we will desiarlize that to convert to string first
                ApartmentDTO model = JsonConvert.DeserializeObject<ApartmentDTO>(Convert.ToString(response.Result));
                //before we return back to the view, we can convert that using automapper to apartmentupdateDTO
                return View(model);
                //if the response is null we return notfound
            }
            return NotFound();


        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApartment(ApartmentDTO model)
        {
           
                var response = await _apartmentService.DeleteAsync<APIResponse>(model.Id);
                if (response != null && response.isSuccess)
                {
                TempData["success"] = "Apartment has been removed";
                //we redirect back to index apartment
                return RedirectToAction(nameof(IndexApartment));
                }
            TempData["ERROR"] = "Error. Please check if everything is okay.";
            //else we return back if the model state is not valid
            return View(model);

        }
    }
}
