using Apartment_API.Database;
using Apartment_API.Models;
using Apartment_API.Models.DTO;
using Apartment_API.Repository.IRepository;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apartment_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private string privateKey;
        public UserRepository(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            privateKey = configuration.GetValue<string>("ApiSettings:Secret");

        }

        public bool isUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
            && u.Password == loginRequestDTO.Password);
            //if user is null or invalid that means that user is not found
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //if user is found we need to generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(privateKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //once we defined all the properties, we need to generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegisterRequestDTO registerRequestDTO)
        {
            //here we are adding the user, here we doing everything to register the new user
            LocalUser user = new()
            {
                UserName = registerRequestDTO.UserName,
                Password = registerRequestDTO.Password,
                Name = registerRequestDTO.Name,
                Role = registerRequestDTO.Role

            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;

        }
    }
}
