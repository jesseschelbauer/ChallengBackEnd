using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> List(CancellationToken ct);
        Task<decimal?> GetCurrentPrice(string symbol, CancellationToken ct);
        Task<Asset?> Get(string symbol, CancellationToken ct);
    }
}