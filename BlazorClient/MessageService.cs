using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient
{
    public interface IMessageService
    {
        event Action<string> OnMessage;

        void SendMessage(string message);
    }

    public class MessageService : IMessageService
    {
        public event Action<string> OnMessage;

        public void SendMessage(string message)
        {
            OnMessage?.Invoke(message);
        }
    }
}