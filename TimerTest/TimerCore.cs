using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerTest
{
    public class TimerCore : BackgroundService, INotificationHandler<ServiceController>
    {
        private readonly ILogger<TimerCore> _logger;
        private CancellationTokenSource _stoppingtoken = new CancellationTokenSource();
        private DateTime CurrentRunningTime = new DateTime();
        private IMediator _mediatr;

        public TimerCore(ILogger<TimerCore> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
            CurrentRunningTime = DateTime.UtcNow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting");
            await DoWork(_stoppingtoken.Token);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(_stoppingtoken.Token);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (!_stoppingtoken.IsCancellationRequested)
            {
                Debug.WriteLine("Ran");
                string[] lines = new string[] { DateTime.UtcNow.ToShortDateString() };
                System.IO.File.AppendAllLines(@"C:\Users\Public\Documents\Service.txt", lines);
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task Handle(ServiceController notification, CancellationToken cancellationToken)
        {
            switch (notification.ActionToBeTaken)
            {
                case ServiceController.Action.Start:
                    MessageBox.Show(notification.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ServiceController.Action.Stop:
                    _stoppingtoken.Cancel(true);
                    string[] lines = new string[] { notification.Message };
                    System.IO.File.AppendAllLines(@"C:\Users\Public\Documents\PingHandler.txt", lines);
                    break;

                case ServiceController.Action.Cancel:
                    MessageBox.Show(notification.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case ServiceController.Action.Status:
                    _mediatr.Publish(new ServiceResponse { Message = CurrentRunningTime.ToLocalTime().ToLongDateString() });
                    break;
            }

            return Task.FromResult(0);
        }
    }
}