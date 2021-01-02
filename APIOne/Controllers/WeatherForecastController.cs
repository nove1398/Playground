using Common;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOne.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBus bus;
        private readonly IJwtAuthenticator authenticator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus _bus, IJwtAuthenticator authenticator)
        {
            _logger = logger;
            this.bus = _bus;
            this.authenticator = authenticator;
        }

        [HttpGet("test")]
        public async Task<string> GetAsync()
        {
            return authenticator.Authenticate("test1", "pass1");
        }

        [HttpGet("testing/{dta}")]
        public string GetString(string dta)
        {
            var input = $"{DateTime.UtcNow.ToShortTimeString()} {dta}";

            var resp = bus.Rpc.Request<Person, PersonResponse>(new Person { Name = input });

            return resp.Data;
        }

        /*public void Send(string queue, string data)
        {
            using IConnection connection = new ConnectionFactory().CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);
            channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
        }*/
    }
}