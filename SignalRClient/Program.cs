using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalRClient
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            var myName = Guid.NewGuid().ToString();

            HubConnection hubConnection = new HubConnectionBuilder()
               .WithAutomaticReconnect()
               .WithUrl($"https://localhost:7171/chathub?name={myName}")
               .Build();

            hubConnection.On("LoungeMessage", (string user, string message) =>
             {
                 Console.ForegroundColor = ConsoleColor.Red;
                 Console.WriteLine($"{user}: {message}");
             });
            hubConnection.On("PrivateMessage", (string fromUser, string message) =>
             {
                 Console.ForegroundColor = ConsoleColor.Green;
                 Console.WriteLine($"{fromUser}: {message}");
             });

            Console.WriteLine("Hello World!");
            var input = "";
            while (input != "quit")
            {
                input = Console.ReadLine().ToString();
                switch (input)
                {
                    case "name":
                    myName = Console.ReadLine().ToString();
                    break;

                    case "connect":
                    await hubConnection.StartAsync();
                    Console.Clear();
                    break;

                    case "disconnect":
                    await hubConnection.StopAsync();
                    //await hubConnection.DisposeAsync();
                    Console.Clear();
                    break;

                    case "changeName":
                    var newName = Console.ReadLine().ToString();
                    await hubConnection.SendAsync("ChangeName", myName, newName);
                    myName = newName;
                    break;

                    case "message":
                    await hubConnection.SendAsync("SendLoungeMessage", myName, Console.ReadLine().ToString());
                    break;

                    case "pm":
                    await hubConnection.SendAsync("SendPM", Console.ReadLine().ToString(), myName, Console.ReadLine().ToString());
                    break;
                }
            }
        }
    }
}