using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SignalRServer.HubFilters;
using SignalRServer.Models;
using SignalRServer.Queue;
using SignalRServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalRServer.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly MyDatabaseContext _context;
        private IBackgroundQueue _queue;

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHubContext<StudentHub> _hub;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, MyDatabaseContext context, IHubContext<StudentHub> hub, IBackgroundQueue queue)
        {
            _logger = logger;
            _context = context;
            _hub = hub;
            _queue = queue;
        }

        [Trimmer]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromForm] Tester data)
        {
            var grades = new List<Grade> { new Grade { Score = 10 }, new Grade { Score = 11 }, new Grade { Score = 12 }, new Grade { Score = 13 } };
            var student = new Student
            {
                Name = "john",
                Age = 12,
                Grades = grades
            };
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
            var val = await _context.Student.AsNoTracking()
                                        .Select(stu => new StudentObject { ID = stu.ID, Grades = stu.Grades.ToList(), Age = stu.Age, Name = stu.Name })
                                        .Take(100)
                                        .ToListAsync();

            _queue.QueueTask(async token =>
            {
                await Task.Delay(1000);
                _logger.LogInformation("Sub Task");
                await Task.Delay(1000);
                _logger.LogInformation("Done");
            });
            _queue.QueueTask(async token =>
            {
                await Task.Delay(1000);
                _logger.LogInformation("Sub Task2");
                await Task.Delay(1000);
                _logger.LogInformation("Done2");
            });
            return Ok(new
            {
                Message = val
            });
        }

        [HttpPost("{id:int}/{stuId:int}")]
        public async Task<IActionResult> UpdateStudent(int id, int stuId)
        {
            var val = await _context.ClassRooms.Include(y => y.Students).FirstOrDefaultAsync(x => x.ClassRoomId == id);
            var stu = await _context.Student.FindAsync(stuId);
            val.Students.Add(stu);

            //val.Students = new List<Student> { stu };

            await _context.SaveChangesAsync();

            var val2 = await _context.Student.AsNoTracking()
                                        .Select(stu => new StudentObject { ID = stu.ID, Grades = stu.Grades.ToList(), Age = stu.Age, Name = stu.Name, ClassRoom = stu.ClassRoom })
                                        .Take(100)
                                        .ToListAsync();

            return Ok(val2);
        }
    }

    public class Tester
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile File { get; set; }
    }
}