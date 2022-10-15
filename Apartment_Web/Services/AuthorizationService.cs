using Apartment_Utility;
using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;



namespace Apartment_Web.Services
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string apartmentUrl;

        public AuthorizationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apartmentUrl = configuration.GetValue<string>("ServiceUrls:ApartmentAPI");
        }


        

        public Task<T> LoginAsync<T>(LoginRequestDTO createObj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = createObj,
                URL = apartmentUrl + "/api/UserAuth/login"
            });
        }
        public Task<T> RegisterAsync<T>(RegisterRequestDTO createObj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = createObj,
                URL = apartmentUrl + "/api/UserAuth/register"
            });
        }

        
    }
}
