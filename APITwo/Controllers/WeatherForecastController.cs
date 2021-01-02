using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APITwo.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            //_bus = bus;
            //_bus.PubSub.Subscribe<Person>("test", HandleTextMessage);
            //Console.WriteLine("Listening for messages. Hit <return> to quit.");
        }

        /*public void Receive(string queue)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "localhost";
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            BasicGetResult result = channel.BasicGet(queue, true);
            if (result != null)
            {
                string data = Encoding.UTF8.GetString(result.Body.ToArray());
                Console.WriteLine(data);
            }
        }*/

        [HttpGet("test2")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}