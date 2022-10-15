using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Apartment_Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO obj = new(); // we need to create new object
            return View(obj); //and return that back
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        //and we will have LoginRequestDTO when the user post the form by hitting submit button
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
           
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public async Task<IActionResult> Register(RegisterRequestDTO obj)
        {
            APIResponse result = await _authorizationService.RegisterAsync<APIResponse>(obj);
            if (result != null && result.isSuccess)
            {
                return RedirectToAction("Login");
            }
            return View(obj);
        }
        public async Task<IActionResult> LogOut()
        {
            return View();
        }
        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
