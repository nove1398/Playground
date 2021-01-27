using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.HubFilters
{
    public class ChatFilter : IHubFilter
    {
        // populated from a file or inline
        private readonly List<string> bannedPhrases = new List<string> { "fuck", "pussy", "pusy", "fck", "bombo", "bumbo" };

        private readonly ILogger<ChatFilter> _logger;

        public ChatFilter(ILogger<ChatFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            _logger.LogInformation($"Calling hub method '{invocationContext.HubMethodName}'");
            var languageFilter = (LanguageFilterAttribute)Attribute.GetCustomAttribute(invocationContext.HubMethod, typeof(LanguageFilterAttribute));
            if (languageFilter != null &&
                invocationContext.HubMethodArguments.Count > languageFilter.FilterArgument &&
                invocationContext.HubMethodArguments[languageFilter.FilterArgument] is string str)
            {
                foreach (var bannedPhrase in bannedPhrases)
                {
                    str = str.Replace(bannedPhrase, "***")
                             .Trim();
                }

                var arguments = invocationContext.HubMethodArguments.ToArray();
                arguments[languageFilter.FilterArgument] = str;
                invocationContext = new HubInvocationContext(invocationContext.Context,
                    invocationContext.ServiceProvider,
                    invocationContext.Hub,
                    invocationContext.HubMethod,
                    arguments);
            }
            return await next(invocationContext);
        }

        // Optional method
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            return next(context);
        }

        // Optional method
        public Task OnDisconnectedAsync(
            HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
        {
            return next(context, exception);
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class LanguageFilterAttribute : Attribute
    {
        public int FilterArgument;

        public LanguageFilterAttribute(int filterArgument)
        {
            FilterArgument = filterArgument;
        }
    }
}