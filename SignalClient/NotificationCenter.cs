using Microsoft.AspNetCore.SignalR.Client;
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
            if (IsConnected())
            {
                return;
            }

            hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl("https://localhost:7171/hubs/students")
                .Build();

            await hubConnection.StartAsync();
        }

        public Task Send(string request, string message) => hubConnection.SendAsync(request, message, message);

        public async ValueTask DisposeAsync() => await hubConnection.DisposeAsync();

        public Task RegisterReceiver(string listenFor, Action<string, string> func) => Task.FromResult(hubConnection.On(listenFor, func));

        public Task RegisterReceiver<T>(string listenFor, Action<T> func) => Task.FromResult(hubConnection.On<T>(listenFor, func));

        public async Task Dispose()
        {
            await hubConnection.StopAsync();
        }

        public bool IsConnected() => hubConnection?.State == HubConnectionState.Connected;

        public Task Send(string request) => hubConnection.SendAsync(request);
    }

    public record UserAgent(string connectionId, string nickname);
}