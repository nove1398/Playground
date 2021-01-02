using Common;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APITwo
{
    public class ConsumerService : IHostedService, IDisposable
    {
        private readonly IBus bus;
        private readonly IBusHub bushub;
        private CancellationTokenSource _stoppingCts;

        public ConsumerService(IBusHub hub, IBus bus)
        {
            this.bus = bus;
            bushub = hub;
            bushub.Value();
        }

        public void Dispose()
        {
            bus?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            //bus.PubSub.Subscribe<Person>("test", (p) => Console.WriteLine($"service {p.Name}"));
            bus.Rpc.Respond<Person, PersonResponse>(resp => new PersonResponse { Data = bushub.Value().GetAwaiter().GetResult() }, _stoppingCts.Token);
            // Otherwise it's running
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _stoppingCts.Cancel();
            bus?.Dispose();

            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine("Service shutting down");
            return Task.CompletedTask;
        }
    }
}