using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;

namespace SignalRServer.HubFilters
{
    public class TrimmerAttribute : Attribute, IActionFilter
    {
        public TrimmerAttribute()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            foreach (var prop in context.ModelState)
            {
                if (prop.Value.RawValue is string)
                {
                    var val = prop.Value.RawValue as string;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var prop in context.ModelState)
            {
                if (prop.Value.RawValue is string)
                {
                    var val = prop.Value.RawValue as string;
                    context.ModelState.SetModelValue(prop.Key, new ValueProviderResult(val.Trim(), CultureInfo.InvariantCulture));
                }
            }
        }
    }
}