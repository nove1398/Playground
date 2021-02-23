using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Security
{
    public class AuthorizedAppHandler : AuthorizationHandler<PermissionsRequirement>
    {
        public const string AuthorizedAppPolicy = nameof(AuthorizedAppPolicy);

        public AuthorizedAppHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            var permissionss = context.User.Claims.Where(x => x.Type == "Permission" && x.Value == requirement.Permission);
            //x.Issuer == "LOCAL AUTHORITY");
            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}