using LinqToLdap;
using LinqToLdap.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LDAP.Controllers
{
    [ApiController]
    [Route("ldap")]
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
        }

        [HttpGet]
        public IActionResult Get()
        {
            var conn = new LdapConnection("192.168.111.76");
            try
            {
                conn.Bind(new NetworkCredential("efranklin", "Jerome1398@", "MTWDOMAIN"));
            }
            catch (LdapException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Invlaid credentials");
            }

            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [HttpGet("test")]
        public async Task<IActionResult> Get2()
        {
            IDirectoryContext context = new DirectoryContext();
            var user = new ADUser();
            user = context.Query<ADUser>()
                .FirstOrDefault(u => u.FirstName == "rohan");

            return Ok(new JsonResult(new
            {
                user.Guid,
                user.CommonName,
                user.FirstName,
                user.LastName,
                CreatedAt = user.WhenCreated.ToShortDateString(),
                UpdatedAt = user.WhenChanged.ToShortDateString(),
                user.Title,
                user.Office,
                user.TelephoneNumber,
                user.MobileNumber,
                user.Extension,
                user.EmployeeNumber,
                user.EmployeeId,
                user.Department,
                user.Supervisor,
                user.TitleDescription,
                user.Notes,
                user.Email,
            }).Value);
        }
    }
}