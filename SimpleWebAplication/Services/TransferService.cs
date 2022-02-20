using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class TransferService : BaseService, ITransferService
    {
        private readonly IUserRepository _userRepository;

        public TransferService(IUserRepository userAssetRepository, IUserInfoService userInfoService) : base(userInfoService)
        {
            _userRepository = userAssetRepository;
        }

        public async Task<ServiceResult<TransferResponse>> Execute(TransferRequest request, CancellationToken ct)
        {

            var user = await _userRepository.GetByAccountId(request.Target.Account, ct).ConfigureAwait(false);

            if (user == null)
                return ServiceResultResponse409Message.Create("Conta não encontrada");

            if(user.Cpf != request.Origin.Cpf)
                return ServiceResultResponse409Message.Create("Apenas transferências para o mesmo cpf são permitidas");

            if(request.Amount <= 0  )
                return ServiceResultResponse409Message.Create("Não é possível transferir um valor negativo");

            user.IncreaseBalance(request.Amount);

            await _userRepository.Update(user, ct).ConfigureAwait(false);

            return new TransferResponse();
        }
    }
}
