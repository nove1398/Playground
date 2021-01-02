using Common;
using EasyNetQ;
using System;

namespace APITwo
{
    public interface IRabHub
    {
        string Print();
    }

    public class RabHub : IRabHub
    {
        public RabHub(IBus bus)
        {
            bus.PubSub.Subscribe<Person>("test", HandleTextMessage);

            Console.WriteLine("Listening for messages from the console");
            Console.ReadLine();
        }

        private void HandleTextMessage(Person textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Name);
            Console.ResetColor();
        }

        public string Print()
        {
            return "hello";
        }
    }
}