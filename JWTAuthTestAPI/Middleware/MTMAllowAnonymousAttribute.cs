using System;

namespace JWTAuthTestAPI.Middleware
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MTMAllowAnonymousAttribute : Attribute
    {
    }
}