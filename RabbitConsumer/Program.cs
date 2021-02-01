using Common;
using EasyNetQ;
using System;
using System.Threading.Tasks;

namespace RabbitConsumer
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            using var bus = RabbitHutch.CreateBus("host=localhost");
            bus.PubSub.Subscribe<Person>("test", HandleTextMessage);

            Console.WriteLine("Listening for messages from the console");
            Console.ReadLine();
        }

        private static void HandleTextMessage(Person textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Name);
            Console.ResetColor();
        }
    }
}