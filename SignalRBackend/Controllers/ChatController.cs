using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRBackend.Hubs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IHubContext<ChatHub> chatHub, ILogger<ChatController> logger)
        {
            _chatHub = chatHub;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            //await _chatHub.Clients.All.SendAsync("ReceiveMessage", user, message);\

            return Ok($"requested [ {Request.Cookies.Count} ]   {Request.Headers["origin"]} {Request.Cookies["token"]} {Request.Cookies["refresh"]}");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync()
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Path = "/",
                SameSite = SameSiteMode.Lax
            };
            Response.Cookies.Append("token", Guid.NewGuid().ToString(), options);
            Response.Cookies.Append("refresh", Guid.NewGuid().ToString(), options);

            var claims = new List<Claim>
                                {
                                  new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
                                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              authProperties);
            return Redirect("https://localhost:5001/");
        }
    }
}