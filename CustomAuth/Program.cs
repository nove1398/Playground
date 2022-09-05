using CustomAuth;
using CustomAuth.Auth;
using CustomAuth.PolicyRequirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "AuthSchemeName"
    });
    option.AddSecurityDefinition("x-user-key", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Please enter a valid id",
        Name = "x-user-key",
        Scheme = "x-user-key"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization"
                },
                Name = "AuthSchemeName",
            },
            new[]{"AuthSchemeName","x-user-key"}
        }
    });
});
builder.Services.AddDataProtection();
builder.Services.AddAuthentication(AuthConstants.AuthSchemeName)
    .AddScheme<AuthOptions, AuthHandler>(AuthConstants.AuthSchemeName, _ =>{});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAccess", policy =>
    {
        policy.RequireRole("Admin");
    });
    options.AddPolicy("WriteAccess", policy =>
    {
        policy.RequireClaim("compliance.write");
        policy.RequireAssertion(x =>
            x.User.Claims.FirstOrDefault(c => c.Type == "compliance.write" && c.Value == "true") != default);
    });
    options.AddPolicy("DeleteAccess", policy =>
    {
        policy.RequireClaim("compliance.write");
        policy.RequireAssertion(x =>
            x.User.Claims.FirstOrDefault(c => c.Type == "compliance.delete" && c.Value == "true") != default);
    });
    options.AddPolicy("ComplianceDeleter", policy =>
    {
        policy.AddRequirements(new ReadWriteUpdateRequirement(new[] { UserAbilities.Create, UserAbilities.Delete, UserAbilities.View, UserAbilities.Edit}, "compliance"));
    });
});
builder.Services.AddSingleton<UserData>();
builder.Services.AddSingleton<IAuthorizationHandler, ReadWriteUpdateHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();