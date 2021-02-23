using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class PermissionsFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private string[] PermissionName { get; }
        private readonly ILogger<PermissionsFilterAttribute> _logger;

        public PermissionsFilterAttribute(params string[] permission)
        {
            PermissionName = permission;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            var service = filterContext.HttpContext.RequestServices.GetService(typeof(ILogger<PermissionsFilterAttribute>)) as ILogger<PermissionsFilterAttribute>;
            var action = filterContext.ActionDescriptor as ControllerActionDescriptor;
            service.LogInformation(action.ControllerName);

            if (filterContext == null)
                return Task.CompletedTask;

            if (filterContext.HttpContext.User == null)
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new JsonResult(new { Error = "Must be logged in" });

                return Task.CompletedTask;
            }

            if (!filterContext.HttpContext.User.Claims.Any(c => c.Type == "Permission" && PermissionName.Any(pn => pn == c.Value)))
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult(new { Error = "No access" });

                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}