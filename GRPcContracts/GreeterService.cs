using ProtoBuf.Grpc;
using System.Threading.Tasks;

namespace GRPcContracts
{
    public class GreeterService : IGreeterService
    {
        public Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default)
        {
            return Task.FromResult(
                new HelloReply
                {
                    Message = $"Hello 4 {request.Name}"
                });
        }
    }
}