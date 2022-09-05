using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CustomAuth.PolicyRequirements;

public class ReadWriteUpdateRequirement : IAuthorizationRequirement
{
    public UserAbilities[] RequiredAbilities { get; }
    public string Module { get; }
    public ReadWriteUpdateRequirement(UserAbilities[] abilities, string module)
    {
        RequiredAbilities = abilities;
        Module = module;
    }
}

public class ReadWriteUpdateHandler : AuthorizationHandler<ReadWriteUpdateRequirement>
{
    private readonly ILogger<ReadWriteUpdateHandler> _logger;

    public ReadWriteUpdateHandler(ILogger<ReadWriteUpdateHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadWriteUpdateRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == null || !context.User.Identity.IsAuthenticated)
        {
            return Task.CompletedTask;
        }

        var userClaims = context.User.Claims.Where(x=>x.Type.StartsWith(requirement.Module)).Select(item => item.ToString()).ToList();
        var requiredClaims = requirement.RequiredAbilities.Select(c => new Claim($"{requirement.Module}.{c.ToString()}".ToLower(), "true").ToString() ).ToList();

        foreach (var item in requiredClaims.Except(userClaims))
        {
            _logger.LogInformation(item);
        }
        
        if (requiredClaims.All(x => userClaims.Contains(x)))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        context.Fail();
        return Task.CompletedTask;
    }
}


public enum UserAbilities
{
    View,
    Create,
    Edit,
    Delete
}