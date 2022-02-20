using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface IUserPositionService
    {
        Task<ServiceResult<UserPositionResponse>> GetCurrent(string userId, CancellationToken ct);
    }
}