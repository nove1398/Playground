using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GRPcContracts
{
    [ServiceContract]
    public interface IGreeterService
    {
        [OperationContract]
        Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default);
    }
}