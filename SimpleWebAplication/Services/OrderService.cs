using SimpleWebAplication.EndpointsDefinitions;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class OrderService : BaseService, IOrderService 
    {
        private readonly IUserAssetRepository _userAssetRepository;
        public IAssetService _assetService;
        private readonly IUserRepository _userRepository;

        public OrderService(IUserRepository userRepository, IUserAssetRepository userAssetRepository, IAssetService assetService, IUserInfoService userInfoService) :base(userInfoService)
        {
            _userAssetRepository = userAssetRepository;
            _assetService = assetService;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<UserAsset>> Create(OrderRequest request, CancellationToken ct)
        {
            var userAsset = Map(request);
            
            var asset = await _assetService.Get(request.Symbol, ct).ConfigureAwait(false);

            if(asset == null)
                return ServiceResultResponse409Message.Create("Ativo inválido");

            var accountBalance = await UserInfoService.GetAccountBalance(User.Id, ct).ConfigureAwait(false);

            var orderValue = request.Amount * asset.Price;

            if (orderValue > accountBalance)
                return ServiceResultResponse409Message.Create("Saldo insuficiente");

            userAsset.UserId = User.Id;
            userAsset.CurrentPrice = asset.Price;

            var user = await _userRepository.Get(User.Id, ct).ConfigureAwait(false);

            user!.DecreaseBalance(orderValue);

            await _userRepository.Update(User, ct).ConfigureAwait(false);

            return await _userAssetRepository.Create(userAsset, ct).ConfigureAwait(false);
        }

        private UserAsset Map(OrderRequest request) 
        {
            return new UserAsset { Amount = request.Amount, Symbol = request.Symbol };
        }   
    }
}
