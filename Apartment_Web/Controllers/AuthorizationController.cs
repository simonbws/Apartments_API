using Apartment_Utility;
using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            //we have auth service, inside there we have login async which expects loginrequests dto and that will return back apiresponse
            APIResponse response = await _authorizationService.LoginAsync<APIResponse>(obj);
            if (response != null && response.isSuccess)
            {
                LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Errors.FirstOrDefault());
                return View(obj);
            }
           
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
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, ""); // we need a key name that is inside session token and we clear session
            return RedirectToAction("Index", "Home");//we redirect back to Index action and that will be inside home controller
        }
        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}
