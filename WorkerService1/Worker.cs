using Common;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            this.bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bus.Rpc.Respond<Person, PersonResponse>(resp => new PersonResponse { Data = "Worker Service" }, stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}