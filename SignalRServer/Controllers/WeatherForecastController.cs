using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SignalRServer.Models;
using SignalRServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalRServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly MyDatabaseContext _context;

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHubContext<StudentHub> _hub;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MyDatabaseContext context, IHubContext<StudentHub> hub)
        {
            _logger = logger;
            _context = context;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var grades = new List<Grade> { new Grade { Score = 10 }, new Grade { Score = 11 }, new Grade { Score = 12 }, new Grade { Score = 13 } };
            var student = new Student
            {
                Name = "john",
                Age = 12,
                Grades = grades
            };
            _context.Student.Add(student);
            _context.SaveChanges();
            var val = await _context.Student.AsNoTracking()
                                            .Select(stu => new StudentObject { ID = stu.ID, Grades = stu.Grades.ToList(), Age = stu.Age, Name = stu.Name })
                                            .Take(100)
                                            .ToListAsync();

            return Ok(new
            {
                Message = val
            });
        }
    }
}