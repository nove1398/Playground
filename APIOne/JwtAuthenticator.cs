using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIOne
{
    public class JwtAuthenticator : IJwtAuthenticator
    {
        private readonly IConfiguration config;

        private Dictionary<string, string> Users = new Dictionary<string, string>
        {
            {"test1","pass1" },
            {"test2","pass2" },
            {"test3","pass3" }
        };

        public JwtAuthenticator(IConfiguration config)
        {
            this.config = config;
        }

        public string Authenticate(string username, string password)
        {
            if (!Users.Any(x => x.Key == username && x.Value == password))
            {
                return "Not Found user";
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, 1+""),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(config.GetSection("jwtSettings")["Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = config.GetSection("jwtSettings")["Audience"],
                Issuer = config.GetSection("jwtSettings")["Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}