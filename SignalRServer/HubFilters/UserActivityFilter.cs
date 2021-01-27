using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace SignalRServer.HubFilters
{
    public class UserActivityFilter : IActionFilter
    {
        private readonly ILogger<UserActivityFilter> _logger;

        public UserActivityFilter(ILogger<UserActivityFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var queryParams = "";
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var user = context.HttpContext.User.Identity.Name;
            var url = $"{controllerName}/{actionName}".ToLower();

            if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                queryParams = context.HttpContext.Request.QueryString.Value;
            }

            var data = context.ActionArguments;

            /* foreach (var item in queryObj)
             {
                 //each param in action
                 var myPropertyInfo = item.Value.GetType().GetProperties();
                 foreach (var prop in myPropertyInfo)
                 {
                     object propValue = prop.GetValue(item.Value, null);
                     var type = propValue.GetType().Name;
                     _logger.LogInformation($"{prop.Name} ");
                 }
             }*/

            _logger.LogInformation(user);
            _logger.LogInformation(url);
            _logger.LogInformation(ipAddress);
            _logger.LogInformation($"{queryParams} ");
            _logger.LogInformation(JsonSerializer.Serialize(data));

            //Store data gathered in DB as ActivityModel
        }
    }
}