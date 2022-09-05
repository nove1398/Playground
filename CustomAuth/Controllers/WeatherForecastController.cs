using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CustomAuth.Controllers;

[Authorize(AuthenticationSchemes = AuthConstants.AuthSchemeName)]
[ApiController]
[Route("api")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly UserData _userData;
    
    public WeatherForecastController(ILogger<WeatherForecastController> logger, UserData userData)
    {
        _logger = logger;
        _userData = userData;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserModel model)
    {
        if (model.Name == "")
            return BadRequest("Empty name");
        if(model.Email == "")
            return BadRequest("Empty email");

        if (!_userData.AddUser(new User(model.Name, model.Email, model.Id, model.Username, model.Password)))
            return BadRequest("Failed to add user");
        
        await Task.Delay(500);
        return Ok("User added");
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (model.Username == "")
            return BadRequest("Empty username");
        if(model.Password == "")
            return BadRequest("Empty password");

        var output = _userData.LoginUser(model.Username, model.Password);
        await Task.Delay(500);

        return Ok(new { Data = output, Key = "AAABBBCCC123" });
    }

    [Authorize(Roles = "Admin", Policy = "WriteAccess")]
    [HttpGet("secret")]
    public async Task<IActionResult> SecretData()
    {
        await Task.Delay(1000);
        return Ok(new { Data = "Secret data found!" });
    }
    
    [Authorize(Roles = "Admin", Policy = "ComplianceDeleter")]
    [HttpDelete("secret")]
    public async Task<IActionResult> SecretDataDelete()
    {
        await Task.Delay(1000);
        return Ok(new { Data = "Secret data deleted!" });
    }
}


public class UserModel
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public readonly int Id = new Random().Next(1, 100);
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}


public class LoginModel
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
