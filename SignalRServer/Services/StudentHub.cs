using Microsoft.AspNetCore.SignalR;
using SignalRServer.HubFilters;
using SignalRServer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Services
{
    public class StudentHub : Hub<IStudentHub>
    {
        public Task SendUserlist() => Clients.Caller.SendUserlist(UserHandler.AllAgentsInDescendingOrder());

        public Task SendBroadcastMessage(string message) => Clients.All.SendBroadcastMessage(message);

        public Task SendStudentInfo(StudentObject student) => Clients.All.SendStudentInfo(student);

        public Task UpdateNickname(string newName)
        => UserHandler.ChangeNickname(Context.ConnectionId, newName)
                ? Clients.All.UpdateNickname(newName)
                : Clients.Caller.SendBroadcastMessage("Failed to update name");

        public Task GetParticipants() => Clients.All.SendParticipants(UserHandler.AllAgentsInDescendingOrder().Select(u => u.nickname).ToList());

        [LanguageFilter(filterArgument: 0)]
        public Task SendUserMessage(string user, string message)
            => UserHandler.FindAgent(user) == null
            ? Clients.Caller.SendMessage("target user not found")
            : Clients.Client(UserHandler.FindAgent(user).connectionId).SendMessage(message);

        public override async Task OnConnectedAsync()
        {
            var nickName = Context.GetHttpContext().Request.Cookies["nickname"];
            UserHandler.AddAgent(new UserAgent(Context.ConnectionId, nickName));
            await Clients.All.SendBroadcastMessage($"Connected {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.RemoveAgent(Context.ConnectionId);
            await Clients.All.SendBroadcastMessage("Disconnected from server");
            await base.OnDisconnectedAsync(exception);
        }
    }
}