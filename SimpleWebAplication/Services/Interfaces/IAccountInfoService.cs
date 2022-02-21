using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface IAccountInfoService
    {
        Task<ServiceResult<AccountInfoResponse>> Get(CancellationToken ct);
    }
}