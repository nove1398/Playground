using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace JWTAuthTest.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MTMAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<MTMAllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (IEnumerable<Claim>)context.HttpContext.Items["User"];

            if (user == null)
            {
                // not logged in
                context.Result = new UnauthorizedResult();
            }
        }
    }
}