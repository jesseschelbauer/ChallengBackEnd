using SimpleWebAplication.Models;

public interface IAssetRepository
{
    Task<decimal?> GetPrice(string symbol, CancellationToken ct);
    Task<IEnumerable<Asset>> List(CancellationToken ct);
    Task<Asset?> Get(string symbol, CancellationToken ct);
}