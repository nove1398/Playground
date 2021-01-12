using MediatR;

namespace TimerTest
{
    public class ServiceResponse : IRequest<ServiceResponse>
    {
        public string Message;
    }
}