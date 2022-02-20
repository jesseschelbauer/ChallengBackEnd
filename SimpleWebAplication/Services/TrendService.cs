using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class TrendService : ITrendService
    {
        private readonly IAssetService _assetService;

        public TrendService(IAssetService assetService)
        {
            _assetService = assetService;
        }
        public async Task<IEnumerable<TrendResponse>> GetTop(int count, CancellationToken ct)
        {
            var assets = await _assetService.List(ct).ConfigureAwait(false);

            return assets.Take(count).Select(Map);
        }

        private TrendResponse Map(Asset asset) 
        {
            /// Não sei a nomenclatura padrão. para uma collection de ações então estou simulando que vai haver um map entre os registros do banco e o response.
            return new TrendResponse { Symbol = asset.Symbol, CurrentPrice = asset.Price };
        }
    }
}
