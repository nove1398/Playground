using Microsoft.AspNetCore.Authorization;

namespace CustomAuthenticationFilter.Security
{
    public class PermissionsRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionsRequirement(string permission = default)
        {
            Permission = permission;
        }

        public PermissionsRequirement()
        {
        }
    }
}