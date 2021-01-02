using Common;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APIOne
{
    public class Backer : IHostedService, IDisposable
    {
        private readonly IBus bus;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public Backer()
        {
            bus = RabbitHutch.CreateBus("host=localhost");
            bus.PubSub.Subscribe<Person>("test", (p) => Console.WriteLine($"service {p.Name}"));
        }

        public void Dispose()
        {
            bus?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Otherwise it's running
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            bus?.Dispose();
            return Task.CompletedTask;
        }
    }
}