using Microsoft.AspNetCore.Authentication;

namespace CustomAuth.Auth;

public class AuthOptions : AuthenticationSchemeOptions
{
    public string ApiKey { get; set; } = "AAABBBCCC123"; 
}