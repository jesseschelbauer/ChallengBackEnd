using SimpleWebAplication.EndpointsDefinitions;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class AuthService: IAuthService
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
            var user = await _userRepository.Get(request.Username, ct).ConfigureAwait(false);

            if(user == null || !_bCrypt.Verify(request.Password, user.PasswordHash))
                return ServiceResultResponse404Message.Create("Invalid username or password");
            
            return new LoginRespose() { User = Map(user),    Token = _jwtTokenService.GenerateToken(user) };
        }

        private LoginUserInfo Map(User user) 
        {
            return new LoginUserInfo { CPF = user.Cpf, Name = user.Name };
        }

        public async Task<ServiceResult<RegisterResponse>> RegisterUser(RegisterRequest request, CancellationToken ct) 
        {
            var user = await _userRepository.Get(request.Username, ct).ConfigureAwait(false);

            if (user != null)
                return ServiceResultResponse409Message.Create($"User already taken");

             user = new User() { Username = request.Username, PasswordHash = _bCrypt.HashPassword(request.Password), Cpf= request.CPF };
             user = await _userRepository.Create(user, ct);
            return new RegisterResponse();
        }
    }   
}
