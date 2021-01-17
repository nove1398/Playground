using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRServer.Services
{
    public class StudentWorker : BackgroundService
    {
        private readonly IHubContext<StudentHub, IStudentHub> _studentHub;
        private readonly ILogger<StudentWorker> _logger;
        private readonly IStudentService _studentService;

        public StudentWorker(IHubContext<StudentHub, IStudentHub> studentHub, ILogger<StudentWorker> logger, IStudentService studentService)
        {
            _studentHub = studentHub;
            _logger = logger;
            _studentService = studentService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Running");
            _studentService.StudentUpdate += UpdateReceived;
            await _studentService.RunAsync(stoppingToken);
            _studentService.StudentUpdate -= UpdateReceived;
            _logger.LogInformation("Stopped Running");
        }

        private async void UpdateReceived(object sender, Models.StudentObject e)
        {
            await _studentHub.Clients.All.SendStudentInfo(e);
        }
    }
}