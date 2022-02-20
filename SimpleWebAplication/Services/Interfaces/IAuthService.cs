using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface IAuthService
    {
        Task<ServiceResult<LoginRespose>> Authenticate(LoginRequest loginRequest, CancellationToken ct);
        Task<ServiceResult<RegisterResponse>> RegisterUser(RegisterRequest request, CancellationToken ct);
    }
}
