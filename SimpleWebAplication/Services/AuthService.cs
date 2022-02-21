using SimpleWebAplication.Context;
using SimpleWebAplication.EndpointsDefinitions;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IBCryptService _bCrypt;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService, IBCryptService bCrypt)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _bCrypt = bCrypt;
        }

        public async Task<ServiceResult<LoginRespose>> Authenticate(LoginRequest request, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmail(request.Email, ct).ConfigureAwait(false);

            if (user == null || !_bCrypt.Verify(request.Password, user.PasswordHash))
                return ServiceResultResponse404Message.Create("Invalid username or password");

            return new LoginRespose() { User = Map(user), Token = _jwtTokenService.GenerateToken(user) };
        }

        private LoginUserInfo Map(User user)
        {
            return new LoginUserInfo { CPF = user.Cpf, Name = user.Name, Email = user.Email };
        }

        public async Task<ServiceResult<RegisterResponse>> RegisterUser(RegisterRequest request, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmail(request.Email, ct).ConfigureAwait(false);

            if (user != null)
                return ServiceResultResponse409Message.Create($"User already taken");

            user = Map(request);
            user.PasswordHash = _bCrypt.HashPassword(request.Password);
            user.Account = GenerataAccountNumber();

            user = await _userRepository.Create(user, ct);
            return new RegisterResponse();
        }

        private string GenerataAccountNumber() 
        {
            // Should be removed to another place
            return DataContext.NewAccount.ToString();
        }

        private User Map(RegisterRequest request)
        {
            return new() { Email = request.Email, Cpf = request.Cpf, Name = request.Name };
        }
    }
}
