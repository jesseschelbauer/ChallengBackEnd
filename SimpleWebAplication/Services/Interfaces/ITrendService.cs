using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface ITrendService
    {
        Task<IEnumerable<TrendResponse>> GetTop(int count, CancellationToken ct);
    }
}