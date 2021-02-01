using Common;
using EasyNetQ;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RabbitSend
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var bus = RabbitHutch.CreateBus("host=localhost");
            var input = "";
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };
            ///api/v1/a/testing
            Console.WriteLine("Enter a message. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "q")
            {
                // HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"{input}");
                //var resp = await client.SendAsync(msg);
                // var data = await resp.Content.ReadAsStringAsync();

                var data = await bus.Rpc.RequestAsync<Person, Person>(new Person { Name = input });
                Console.WriteLine(data);
            }
        }
    }
}