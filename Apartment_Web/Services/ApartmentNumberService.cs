using Apartment_Utility;
using Apartment_Web.Models;
using Apartment_Web.Models.DTO;
using Apartment_Web.Services.IServices;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace Apartment_Web.Services
{
    public class ApartmentNumberService : BaseService, IApartmentNumberService // we need to invoke baseservice to
        //to have access to all api calls

    {
        private readonly IHttpClientFactory _clientFactory;
        private string apartmentUrl;

        public ApartmentNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            apartmentUrl = configuration.GetValue<string>("ServiceUrls:ApartmentAPI");
        }

        public Task<T> CreateAsync<T>(ApartmentNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                URL = apartmentUrl + "/api/apartmentNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,      
                URL = apartmentUrl + "/api/apartmentNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                URL = apartmentUrl + "/api/apartmentNumberAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                URL = apartmentUrl + "/api/apartmentNumberAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ApartmentNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                URL = apartmentUrl + "/api/apartmentNumberAPI/" + dto.ApartmentNo,
                Token = token
            });
        }
    }
}
