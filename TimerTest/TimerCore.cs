using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TimerTest
{
    public class TimerCore : BackgroundService
    {
        private readonly ILogger<TimerCore> _logger;

        public TimerCore(ILogger<TimerCore> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine("Starting");

            await DoWork(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(cancellationToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine("Ran");
                string[] lines = new string[] { DateTime.UtcNow.ToShortDateString() };
                System.IO.File.AppendAllLines(@"C:\Users\Public\Documents\Service.txt", lines);
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}