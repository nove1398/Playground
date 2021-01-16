using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task Test(string user, string message)
        {
            await Clients.All.SendAsync("Test", user, message);
        }
    }
}