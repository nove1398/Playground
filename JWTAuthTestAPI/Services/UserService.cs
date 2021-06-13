using JWTAuthTest;
using JWTAuthTestAPI.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthTestAPI.Services
{
    public interface IUserService
    {
        string Authenticate(string model);

        IEnumerable<User> GetAll();

        User GetById(Guid id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Name = "Test"}
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string Authenticate(string model)
        {
            var user = new User { Id = Guid.Parse("e3c1df0e-2271-45bf-bb5f-7fd2a5ec54d9"), Name = model };

            if (model == "fail")
            {
                return null;
            }
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return token;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(Guid id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}