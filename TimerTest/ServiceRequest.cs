using MediatR;

namespace TimerTest
{
    public class ServiceRequest : INotification
    {
        public string Message;
        public Action ActionToBeTaken;

        public enum Action
        {
            Start,
            Stop,
            Cancel,
            Status
        };
    }
}