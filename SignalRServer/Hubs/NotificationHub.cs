using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRServer.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task NotifyUser(string action)
        {
            await Clients.All.SendAsync("Action", action);
        }
    }
}