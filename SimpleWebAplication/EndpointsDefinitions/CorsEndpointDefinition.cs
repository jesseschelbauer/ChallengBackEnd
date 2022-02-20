namespace SimpleWebAplication.EndpointsDefinitions;
public class CorsEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.UseCors();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddCors(options => {
            options.AddDefaultPolicy(
                builder => {
                    builder.
                        AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    public EndpointDefinitionRegistrationPriority RegisterPrior { get { return EndpointDefinitionRegistrationPriority.BeforeAuth; } }
}