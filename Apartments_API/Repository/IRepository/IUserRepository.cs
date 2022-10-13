using Apartment_API.Models;
using Apartment_API.Models.DTO;

namespace Apartment_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
        Task<LocalUser> Register(RegisterRequestDTO registerRequestDTO);
    }
}
