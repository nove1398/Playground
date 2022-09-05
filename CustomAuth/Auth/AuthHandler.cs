using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CustomAuth.Auth;

public class AuthHandler : AuthenticationHandler<AuthOptions>
{
    private readonly UserData _userData;
    
    public AuthHandler(IOptionsMonitor<AuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, UserData userData) : base(options, logger, encoder, clock)
    {
        _userData = userData;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid key"));
        }


        var authKey = Request.Headers[HeaderNames.Authorization].ToString();
        if (authKey != Options.ApiKey)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid key"));
        }

        var userId = Convert.ToInt32(Request.Headers["x-user-key"].ToString());
        var user = _userData.GetUserById(userId);
        if(user == default)
            return Task.FromResult(AuthenticateResult.Fail("Invalid user"));
            
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "User"),
            new Claim("compliance.view", "true"),
            new Claim("compliance.create", "true"),
            //new Claim("compliance.delete", "true"),
            new Claim("compliance.edit", "true"),
        };

        var identity = new ClaimsIdentity(claims, AuthConstants.AuthTypeUser);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}