using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRBackend.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private static ConcurrentDictionary<string, UserAgent> Users = new ConcurrentDictionary<string, UserAgent>();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task SendLoungeMessage(string user, string message)
        {
            var agent = Users.GetOrAdd(user, new UserAgent(user, Context.ConnectionId));
            await Clients.All.LoungeMessage(agent.Name, message);
        }

        public async Task ChangeName(string oldName, string newName)
        {
            var newAgent = new UserAgent(newName, Context.ConnectionId);
            Users.TryRemove(oldName, out UserAgent oldAgent);

            Users.TryAdd(oldName, newAgent);
            await Clients.Others.NameChanged(oldName, newName);
        }

        public async Task SendPM(string toUser, string fromUser, string message)
        {
            Users.TryGetValue(toUser, out UserAgent agent);
            await Clients.Client(agent.ConnectionIds.LastOrDefault()).PrivateMessage(fromUser, message);
        }

        public override Task OnConnectedAsync()
        {
            var tempName = Context.GetHttpContext().Request.QueryString.ToString().Split("&")[0][6..];
            var userId = Context.ConnectionId;
            Users.GetOrAdd(tempName, new UserAgent(tempName, userId));
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            var tempName = Context.GetHttpContext().Request.QueryString.ToString().Split("&")[0][6..];

            if (Users.TryRemove(tempName, out UserAgent agent))
            {
                _logger.LogInformation(agent.Name);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}