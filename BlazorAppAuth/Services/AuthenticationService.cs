using BlazorAppAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppAuth.Services
{
    public interface IAuthenticationService
    {
        User User { get; }
    }

    public class AuthenticationService : IAuthenticationService
    {
        public User User => throw new NotImplementedException();
    }
}