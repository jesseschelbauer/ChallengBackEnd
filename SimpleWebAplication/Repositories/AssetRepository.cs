using SimpleWebAplication.Context;
using SimpleWebAplication.Models;

public class AssetRepository : IAssetRepository
{
    public AssetRepository()
    {
    }

    public async Task<decimal?> GetPrice(string symbol, CancellationToken ct)
    {
        var asset = DataContext.Assets.FirstOrDefault(a => a.Symbol == symbol);
        return await Task.FromResult(asset?.Price ?? null).ConfigureAwait(false);
    }

    public async Task<Asset?> Get(string symbol, CancellationToken ct)
    {
        var asset = DataContext.Assets.FirstOrDefault(a => a.Symbol == symbol);
        return await Task.FromResult(asset).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Asset>> List(CancellationToken ct)
    {
        return await Task.FromResult(DataContext.Assets).ConfigureAwait(false);
    }
}
