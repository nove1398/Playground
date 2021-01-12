using MediatR;

namespace TimerTest
{
    public class ServiceResponse : INotification
    {
        public string Message;
    }
}