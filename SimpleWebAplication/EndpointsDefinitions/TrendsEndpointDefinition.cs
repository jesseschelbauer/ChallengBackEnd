using SimpleWebAplication.Services;

public class TrendsEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.MapGet("/trends", async (ITrendService trendService, CancellationToken ct) => {
            return await trendService.GetTop(5, ct).ConfigureAwait(false);
        });
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<ITrendService, TrendService>();
    }
}
