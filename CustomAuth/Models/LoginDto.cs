using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuth.Models
{
    public class LoginDto
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}