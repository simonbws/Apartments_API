using Microsoft.AspNetCore.Mvc;
using static Apartment_Utility.SD;

namespace Apartment_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string URL { get; set; } 
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
