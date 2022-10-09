using System.Net;

namespace Apartment_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool isSuccess { get; set; }
        public List<string> Errors { get; set; }

        public object Result { get; set; }
    }
}
