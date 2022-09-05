using CustomAuthenticationFilter.Data;
using CustomAuthenticationFilter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Filter
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiAuthFilter : Attribute, IAsyncAuthorizationFilter
    {
        public ApiAuthFilter()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
            {
                return;
            }

            if (!filterContext.HttpContext.Request.Headers.TryGetValue("X-FX-Key", out var apiKey))
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new JsonResult(new { Error = "Error no key provided" });

                return;
            }

            //Validate key
            var _senitFxManager = filterContext.HttpContext.RequestServices.GetRequiredService<ISenitFxManagerService>();

            var currentKey = await _senitFxManager.GetApiKey(apiKey);
            if (currentKey == null)
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new JsonResult(new { Error = "Not authorized" });
            }
        }
    }
}