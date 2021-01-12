using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTest
{
    public static class ServiceCommands
    {
        //Query
        public record Query(QueryType queryType) : IRequest<Response>;

        public enum QueryType
        {
            Start,
            Stop,
            Status
        }

        //Handler example
        public class Handler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        //Response
        public record Response(string message);
    }
}