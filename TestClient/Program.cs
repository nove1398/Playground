using Grpc.Net.Client;
using System;

namespace TestClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            Console.WriteLine("Hello World!");
        }
    }
}