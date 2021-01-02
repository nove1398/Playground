using System;
using System.Security.Cryptography;

namespace APIOne
{
    public class JwtRefreshTokenGenerator : IJwtRefreshTokenGenerator
    {
        public string Generate()
        {
            var randomNum = new byte[32];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(randomNum);
            return Convert.ToBase64String(randomNum);
        }
    }
}