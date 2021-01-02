using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAPI.Controllers
{
    [ApiController]
    [Route("cookieapi")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Get(int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Cookies["CookieMonster"]))
            {
                _logger.LogInformation(_httpContextAccessor.HttpContext.Request.Cookies["CookieMonster"]);
            }

            var rng = new Random();
            var items = Enumerable.Range(0, 5000).Select(index => new WeatherForecast
            {
                Index = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10);
            option.HttpOnly = true;
            option.Domain = "/";
            _httpContextAccessor.HttpContext.Response.Cookies.Append("CoookieMonster", "test_test_test", option);

            var pager = new PaginationBuilder(items.Count, page, pageSize);

            var results = new { pager, data = items.Skip(pager.StartIndex).Take(pager.PageSize) };
            return Ok(new JsonResult(results).Value);
        }
    }
}