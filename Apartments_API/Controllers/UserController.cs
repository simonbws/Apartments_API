using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Apartment_API.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new(); // we initialize the response
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.Errors.Add("Username or password is invalid");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.isSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
            

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            bool ifUserIsUnique = _userRepository.isUniqueUser(model.UserName);
            if (!ifUserIsUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.Errors.Add("Sorry, UserName already exists!");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.Errors.Add("Error while register");
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
