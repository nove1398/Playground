using Grpc.Net.Client;
using GRPcContracts;
using ProtoBuf.Grpc.Client;
using System;

namespace GRPcClient
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();
            var data = Console.ReadLine();

            var reply = await client.SayHelloAsync(new HelloRequest { Name = data });

            Console.WriteLine($"Greeting: {reply.Message}");
            Console.ReadLine();
        }
    }
}