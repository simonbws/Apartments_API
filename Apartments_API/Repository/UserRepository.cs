using Apartment_API.Database;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;

namespace Apartment_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool isUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<LocalUser> Register(RegisterRequestDTO registerRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
