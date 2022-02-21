using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class AccountInfoService : BaseService, IAccountInfoService
    {
        private readonly IUserRepository _userRepository;

        public AccountInfoService(IUserRepository userRepository, IUserInfoService userInfoService) : base(userInfoService)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<AccountInfoResponse>> Get(CancellationToken ct)
        {
            var user = await _userRepository.Get(User.Id, ct).ConfigureAwait(false);

            if (user == null)
                return ServiceResultResponse404Message.Create("User not found");

            return Map(user);
        }

        private AccountInfoResponse Map(User user)
        {
            return new AccountInfoResponse
            {
                Balance = user.AccoutBalance,
                Cpf = user.Cpf,
                Email = user.Email,
                Name = user.Name,
                Number = user.Account
            };
        }
    }
}
