using Microsoft.AspNetCore.SignalR;
using SimpleWebAplication.Models;
using SimpleWebAplication.Services;

namespace SimpleWebAplication.EndpointsDefinitions;

public class WsEndpointDefinition : IEndpointDefinition
{
    public void Define(WebApplication web)
    {
        web.MapHub<TrendsHub>("ws-trends");
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSignalR();
    }
}

public interface ITrendsHub
{
    Task TopTrends(IEnumerable<TrendResponse> topTrends,  CancellationToken ct);
}

public class TrendsHub : Hub<ITrendsHub>
{
    public TrendsHub()
    {       
    }

    public async Task TopTrends(IEnumerable<TrendResponse> topTrends, CancellationToken ct)
    {
        await Clients.All.TopTrends(topTrends, ct).ConfigureAwait(false);
    }
}
