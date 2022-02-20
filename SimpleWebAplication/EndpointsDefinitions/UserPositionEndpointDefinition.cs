using SimpleWebAplication.Repositories;

public class UserPositionEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.MapGet("/userPosition", (IUserPositionRepository repo) => {
            throw new NotImplementedException();
        });
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IUserPositionRepository, UserPositionRepository>();   
    }
}
