using Microsoft.AspNetCore.SignalR;
using SimpleWebAplication.EndpointsDefinitions;
using SimpleWebAplication.Services;

public class TimedHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger;
    private readonly IHubContext<TrendsHub, ITrendsHub> _trendsHub;
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer = null!;

    public TimedHostedService(ILogger<TimedHostedService> logger, IHubContext<TrendsHub, ITrendsHub> trendsHub, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _trendsHub = trendsHub;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {

        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            ITrendService trendService = scope.ServiceProvider.GetRequiredService<ITrendService>();

            var r = new Random();

            var topTrends = await trendService.GetTop(5, CancellationToken.None).ConfigureAwait(false);

            topTrends = topTrends.Select(t =>
            {
                t.CurrentPrice += t.CurrentPrice * (decimal)r.NextDouble();
                return t;
            });

            await _trendsHub.Clients.All.TopTrends(topTrends, CancellationToken.None).ConfigureAwait(false);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}