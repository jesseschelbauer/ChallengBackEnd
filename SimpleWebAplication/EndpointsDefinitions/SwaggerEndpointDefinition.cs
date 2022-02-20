using Microsoft.OpenApi.Models;

public class SwaggerEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication app)
    {

        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My api", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Id = "Bearer", Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string> ()
                }
            });
        });
    }

    public EndpointDefinitionRegistrationPriority RegisterPrior { get { return EndpointDefinitionRegistrationPriority.BeforeAuth; } }
}