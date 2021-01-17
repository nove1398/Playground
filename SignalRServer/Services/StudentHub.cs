using Microsoft.AspNetCore.SignalR;
using SignalRServer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRServer.Services
{
    public class StudentHub : Hub<IStudentHub>
    {
        public Task SendBroadcastMessage(string message)
        {
            return Clients.All.SendBroadcastMessage(message);
        }

        public Task SendStudentInfo(StudentObject student)
        {
            return Clients.All.SendStudentInfo(student);
        }

        public override async Task OnConnectedAsync()
        {
            UserHandler.ConnectionIds.Add(Context.ConnectionId);
            await Clients.All.SendBroadcastMessage("Connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserHandler.ConnectionIds.Remove(Context.ConnectionId);
            await Clients.All.SendBroadcastMessage("Disconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }

    public static class UserHandler
    {
        public static HashSet<string> ConnectionIds = new HashSet<string>();
    }
}