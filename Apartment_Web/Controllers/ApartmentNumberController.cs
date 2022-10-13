using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Models.ViewModel;
using Apartment_Web.Services;
using Apartment_Web.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apartment_Web.Controllers
{
    public class ApartmentNumberController : Controller
    {
        private readonly IApartmentNumberService _apartmentNumberService;
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public ApartmentNumberController(IApartmentNumberService apartmentNumberService, IMapper mapper, IApartmentService apartmentService)
        {
            _apartmentNumberService = apartmentNumberService;
            _mapper = mapper;
            _apartmentService = apartmentService;
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





        public async Task<IActionResult> CreateApartmentNumber()
        {
            ApartmentNumberCreateViewModel apartmentNumberViewModel = new();

            var response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.isSuccess)
            {
                //we convert that to string, then we deserialize that to list od apart dto and we assign that to list
                //result will be ApartmentNumberDTO
                apartmentNumberViewModel.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(apartmentNumberViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateApartmentNumber(ApartmentNumberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _apartmentNumberService.CreateAsync<APIResponse>(model.ApartmentNumber);
                if (response != null && response.isSuccess)
                {
                    return RedirectToAction(nameof(IndexApartmentNumber));
                }
                else
                {
                    if (response.Errors.Count > 0)
                    {
                        ModelState.AddModelError("Errors", response.Errors.FirstOrDefault());
                    }
                }
            }
            var resp = await _apartmentService.GetAllAsync<APIResponse>();
            if (resp != null && resp.isSuccess)
            {
                model.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

            }
            return View(model);

        }

        public async Task<IActionResult> UpdateApartmentNumber(int apartmentId) // we will get aprt id
        {
            ApartmentNumberUpdateViewModel apartmentNumberViewModel = new();
            //then we will pass that id
            var response = await _apartmentNumberService.GetAsync<APIResponse>(apartmentId);
            //and we will retrieve the complete apartment
            if (response != null && response.isSuccess)
            {
                //we will desiarlize that to convert to string first
                ApartmentNumberDTO model = JsonConvert.DeserializeObject<ApartmentNumberDTO>(Convert.ToString(response.Result));
                apartmentNumberViewModel.ApartmentNumber =  _mapper.Map<ApartmentNumberUpdateDTO>(model);
                //before we return back to the view, we can convert that using automapper to apartmentupdateDTO
                
                //if the response is null we return notfound
            }


            response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.isSuccess)
            {
                //we convert that to string, then we deserialize that to list od apart dto and we assign that to list
                //result will be ApartmentNumberDTO
                apartmentNumberViewModel.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(apartmentNumberViewModel);
            }
            return NotFound();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApartmentNumber(ApartmentNumberUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //we update here
                var response = await _apartmentNumberService.UpdateAsync<APIResponse>(model.ApartmentNumber);
                if (response != null && response.isSuccess)
                {
                    //if everything is good we redirect back here
                    return RedirectToAction(nameof(IndexApartmentNumber));
                }
                //else we check error messages
                else
                {
                    if (response.Errors.Count > 0)
                    {
                        ModelState.AddModelError("Errors", response.Errors.FirstOrDefault());
                    }
                }
            }
            //and if anything is not valid we populate dropdown and redirect back
            var resp = await _apartmentService.GetAllAsync<APIResponse>();
            if (resp != null && resp.isSuccess)
            {
                model.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

            }
            return View(model);

        }


        public async Task<IActionResult> DeleteApartmentNumber(int apartmentId) // we will get aprt id
        {
            ApartmentNumberUpdateViewModel apartmentNumberViewModel = new();
            var response = await _apartmentNumberService.GetAsync<APIResponse>(apartmentId);
            if (response != null && response.isSuccess)
            {
                ApartmentNumberDTO model = JsonConvert.DeserializeObject<ApartmentNumberDTO>(Convert.ToString(response.Result));
                apartmentNumberViewModel.ApartmentNumber = _mapper.Map<ApartmentNumberUpdateDTO>(model);
            }
            response = await _apartmentService.GetAllAsync<APIResponse>();
            if (response != null && response.isSuccess)
            {            
                apartmentNumberViewModel.ApartmentList = JsonConvert.DeserializeObject<List<ApartmentDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(apartmentNumberViewModel);
            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteApartmentNumber(ApartmentNumberDeleteViewModel model)
        {
            var response = await _apartmentNumberService.DeleteAsync<APIResponse>(model.ApartmentNumber.ApartmentNo);
            if (response != null && response.isSuccess)
            {
                return RedirectToAction(nameof(IndexApartmentNumber));
            } 
            return View(model);

        }
    }
}
