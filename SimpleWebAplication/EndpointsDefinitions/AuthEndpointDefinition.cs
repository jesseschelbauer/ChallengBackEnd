namespace SimpleWebAplication.EndpointsDefinitions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleWebAplication.Models;
using SimpleWebAplication.Repositories;
using SimpleWebAplication.Services;
using SimpleWebAplication.Validators;
using System.Text;

public class AuthEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapPost("/auth", async (IValidator<LoginRequest> validator, IAuthService authService, [FromBody] LoginRequest request, CancellationToken ct) =>
        {
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await authService.Authenticate(request, ct).ConfigureAwait(false);

            if (!result.IsOk)
                return Results.Extensions.ServiceResult(result.ErrorResponse);

            return Results.Ok(new LoginRespose { User = result.Result!.User,  Token = result.Result!.Token });
        }).AllowAnonymous();

        app.MapPost("/register", async (IAuthService authService, IValidator<RegisterRequest> validator, [FromBody] RegisterRequest request, CancellationToken ct) =>
        {
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await authService.RegisterUser(request, ct).ConfigureAwait(false);

            if (!result.IsOk)
                return Results.Extensions.ServiceResult(result.ErrorResponse);

            return Results.Ok(result.Result);
        }).AllowAnonymous();
    }

    public void DefineServices(IServiceCollection services)
    {

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBCryptService, BCryptService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserInfoService, UserInfoService>();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
        });

        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        services.AddAuthentication(s =>
        {
            s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(t =>
        {
            t.RequireHttpsMetadata = false;
            t.SaveToken = true;
            t.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    }

    public EndpointDefinitionRegistrationPriority RegisterPrior { get { return EndpointDefinitionRegistrationPriority.Auth; } }
}
