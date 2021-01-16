using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SignalClient
{
    public class NotificationCenter : INotificationCenter, IAsyncDisposable
    {
        private HubConnection hubConnection;

        public NotificationCenter()
        {
        }

        public Task Notification(string data)
        {
            return Task.CompletedTask;
        }

        public async Task InitSocket()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7171/chathub")
                .Build();

            await hubConnection.StartAsync();
        }

        public Task Send(string request, string message) => hubConnection.SendAsync(request, message, message);

        public async ValueTask DisposeAsync()
        {
            await hubConnection.DisposeAsync();
        }

        public Task RegisterReceiver(string listenFor, Action<string, string> func)
        {
            hubConnection.On(listenFor, func);
            return Task.CompletedTask;
        }

        public bool IsConnected => hubConnection.State == HubConnectionState.Connected;
    }
}