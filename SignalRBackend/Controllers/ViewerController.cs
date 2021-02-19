using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewerController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> IndexAsync()
        {
            var claims = new List<Claim>
                                {
                                  new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                                  new Claim(ClaimTypes.Name, "Joe"),
                                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              authProperties);

            //return Redirect("https://localhost:5001/");
            return Ok();
        }
    }
}