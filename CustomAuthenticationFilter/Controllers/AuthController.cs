using CustomAuthenticationFilter.Filter;
using CustomAuthenticationFilter.Models;
using CustomAuthenticationFilter.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomAuthenticationFilter.Controllers
{
    [Route("api2")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public string ModuleName { get; set; } = "AuthService";

        public AuthController()
        {
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            List<Claim> claimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,  MockDB.UserModels[1].Id+""),
                new Claim(ClaimTypes.Name, MockDB.UserModels[1].Name),
                new Claim(ClaimTypes.Role, MockDB.RoleModels[1].RoleName),
                new Claim("Permission", MockDB.PermissionModels["Read"].Name),
                new Claim("Permission", MockDB.PermissionModels["Write"].Name),
                new Claim("Permission", MockDB.PermissionModels["Delete"].Name),
            };
            var idententity = new ClaimsIdentity(claimList, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(idententity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddDays(1) });
            return Ok("logged");
        }

        [HttpGet("products/view")]
        [PermissionsFilter("Permission.Authy.Read")]
        public IActionResult Viewer()
        {
            return Ok(User.Identity.Name + " read data");
        }

        [HttpPost("products/write")]
        [PermissionsFilter("Permission.Authy.Read", "Permission.Authy.Write")]
        public IActionResult Writer()
        {
            return Ok(User.Identity.Name + " wrote data");
        }

        [HttpGet("products/delete")]
        [PermissionsFilter("Permission.Authy.Delte")]
        public IActionResult Deleter()
        {
            return Ok(DateTime.Now.ToLongDateString());
        }

        [HttpGet("services")]
        public IActionResult GetServices()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var values = asm.GetTypes()
                            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)) //filter controllers
                            .Select(type => type);

            var output = new List<string>();
            foreach (var controller in values)
            {
                output.Add(controller.GetProperty("ModuleName").GetValue("ModuleName", null).ToString());
            }
            return Ok(output);
        }

        [HttpPost("add/permission")]
        public IActionResult AddPermissions([FromBody] PermissionRequest request)
        {
            var data = MockDB.AddNewPermission(request.Module, request.Ability);

            return Ok(data);
        }
    }
}