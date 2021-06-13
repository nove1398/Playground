using System;

namespace JWTAuthTest.Middleware
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MTMAllowAnonymousAttribute : Attribute
    {
    }
}