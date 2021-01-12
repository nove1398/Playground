using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TimerTest.ServiceCommands;

namespace TimerTest
{
    public class TimerCore : BackgroundService, IRequestHandler<Query, Response>
    {
        private readonly ILogger<TimerCore> _logger;
        private CancellationTokenSource _stoppingtoken = new CancellationTokenSource();
        private DateTime CurrentRunningTime;

        public TimerCore(ILogger<TimerCore> logger)
        {
            _logger = logger;
            CurrentRunningTime = DateTime.UtcNow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting");
            await DoWork(_stoppingtoken.Token);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            string[] lines = new string[] { $"Stopped at: {DateTime.UtcNow}" };
            System.IO.File.AppendAllLines(@"C:\Users\Public\Documents\ServiceStopped.txt", lines);
            await base.StopAsync(_stoppingtoken.Token);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine("Ran");
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            switch (request.queryType)
            {
                case QueryType.Start:
                    return Task.FromResult(new Response("Service started"));

                case QueryType.Stop:
                    string[] lines = new string[] { $"Stopped at: {DateTime.UtcNow}" };
                    System.IO.File.AppendAllLines(@"C:\Users\Public\Documents\ServiceStopped.txt", lines);
                    _stoppingtoken.Cancel();
                    return Task.FromResult(new Response("Service started"));

                case QueryType.Status:
                    var since = CurrentRunningTime - DateTime.UtcNow;
                    string[] lines2 = new string[] { $"up since {since.TotalSeconds}" };
                    System.IO.File.AppendAllLines(@"C:\Users\Public\Documents\ServiceStatus.txt", lines2);
                    return Task.FromResult(new Response(lines2[0]));
            }
            return Task.FromResult(new Response("Invalid request"));
        }
    }
}