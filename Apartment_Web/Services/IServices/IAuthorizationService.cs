using Apartment_Web.Models.DTO;

namespace Apartment_Web.Services.IServices
{
    public interface IAuthorizationService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO createObj);
        Task<T> RegisterAsync<T>(RegisterRequestDTO createObj);
    }
}
