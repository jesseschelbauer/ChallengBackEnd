using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class AssetService: IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<decimal?> GetCurrentPrice(string symbol, CancellationToken ct) 
        {
            return await _assetRepository.GetPrice(symbol, ct).ConfigureAwait(false);
        }

        public async Task<Asset?> Get(string symbol, CancellationToken ct)
        {
            return await _assetRepository.Get(symbol, ct).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Asset>> List(CancellationToken ct)
        {
            return await _assetRepository.List(ct).ConfigureAwait(false);
        }
    }
}
