using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface IUserInfoService
    {
        User User { get; }
        Task<decimal> GetAccountBalance(int id, CancellationToken ct);
    }
}