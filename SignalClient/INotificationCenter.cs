using System;
using System.Threading.Tasks;

namespace SignalClient
{
    public interface INotificationCenter
    {
        Task Notification(string data);

        Task InitSocket();

        Task Send(string request, string message);

        Task RegisterReceiver(string listenFor, Action<string, string> func);

        Task RegisterReceiver<T>(string listenFor, Action<T> func);

        Task Dispose();

        bool IsConnected();
    }
}