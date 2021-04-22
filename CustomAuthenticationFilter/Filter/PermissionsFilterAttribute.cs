using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class PermissionsFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private string[] PermissionName { get; }

        public PermissionsFilterAttribute(params string[] permission)
        {
            PermissionName = permission;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            var logger = filterContext.HttpContext.RequestServices.GetService(typeof(ILogger<PermissionsFilterAttribute>)) as ILogger<PermissionsFilterAttribute>;
            var action = filterContext.ActionDescriptor as ControllerActionDescriptor;

            if (filterContext == null)
                return Task.CompletedTask;

            if (filterContext.HttpContext.User == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new JsonResult(new { Error = "Must be logged in" });

                return Task.CompletedTask;
            }

            var requiredPermissions = PermissionName
                .OrderByDescending(i => i)
                .ToList();
            var userPermissions = filterContext.HttpContext.User.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .OrderByDescending(i => i)
                .ToList();

            if (!requiredPermissions.All(pn => userPermissions.Contains(pn)))
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult(new { Error = "No access" });//new ForbidResult();

                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}