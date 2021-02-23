using CustomAuthenticationFilter.Filter;
using CustomAuthenticationFilter.Security;
using CustomAuthenticationFilter.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        public string ModuleName { get; set; } = "AuthService";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISenitFxManagerService _fxManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISenitFxManagerService fxManager)
        {
            _logger = logger;
            _fxManager = fxManager;
        }

        [HttpGet("customauth")]
        [Authorize(Policy = AuthorizedAppHandler.AuthorizedAppPolicy)]
        public IActionResult AuthorizedEndPoint()
        {
            return Ok("Access Granted");
        }

        [HttpGet]
        [ApiAuthFilter]
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

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateKey()
        {
            var key = await _fxManager.GenerateNewApiKey();

            return Ok(key);
        }

        [ApiAuthFilter]
        [HttpPut("disable")]
        public async Task<IActionResult> KillKey([FromForm] string key)
        {
            var success = await _fxManager.KillApiKey(key);

            return Ok(success ? $"disabled {key}" : "not found");
        }

        [ApiAuthFilter]
        [HttpPut("enable")]
        public async Task<IActionResult> ReviveKey([FromForm] string key)
        {
            var success = await _fxManager.EnableApiKey(key);

            return Ok(success ? $"enabled {key}" : "not found");
        }

        [ApiAuthFilter]
        [HttpDelete("bombkeys")]
        public async Task<IActionResult> ReviveKey()
        {
            await _fxManager.KillAllKeys();

            return Ok("Disabled all keys");
        }
    }
}