using SimpleWebAplication.Models;

public interface IUserRepository 
{
    Task<User?> Get(int id, CancellationToken CancellationToken);
    Task<User?> GetByEmail (string username, CancellationToken CancellationToken);
    Task<User> Create(User user, CancellationToken CancellationToken);
    Task<decimal> GetAccountBalance(int id, CancellationToken ct);
    Task<User?> GetByAccountId(string account, CancellationToken ct);
    Task<bool> Update(User user, CancellationToken ct);
}