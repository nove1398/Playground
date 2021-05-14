using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailKitSender.Services
{
    public class MailFetchService : BackgroundService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<MailFetchService> _logger;

        public MailFetchService(IEmailService emailService, ILogger<MailFetchService> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BG Service");

            while (!stoppingToken.IsCancellationRequested)
            {
               await _emailService.ReadMail(10);
                await Task.Delay(TimeSpan.FromMinutes(2));
            }
        }
    }
}