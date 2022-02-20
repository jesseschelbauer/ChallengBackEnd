using SimpleWebAplication.Context;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }
        public async Task<User> Create(User user, CancellationToken ct)
        {
            user.Id = DataContext.NewId;
            DataContext.Users.Add(user);
            return await Task.FromResult(user).ConfigureAwait(false);
        }

        public async Task<User?> GetByEmail(string email, CancellationToken ct)
        {
            return await Task.FromResult(DataContext.Users.FirstOrDefault(u => u.Email == email)).ConfigureAwait(false);
        }

        public async Task<User?> Get(int id, CancellationToken ct)
        {
            return await Task.FromResult(DataContext.Users.FirstOrDefault(u => u.Id == id)).ConfigureAwait(false);
        }

        public async Task<decimal> GetAccountBalance(int id, CancellationToken ct)
        {
            return await Task.FromResult(DataContext.Users.FirstOrDefault(u => u.Id == id)?.AccoutBalance ?? 0).ConfigureAwait(false);
        }

        public async Task<User?> GetByAccountId(string account, CancellationToken ct)
        {
            return await Task.FromResult(DataContext.Users.FirstOrDefault(u => u.Account == account)).ConfigureAwait(false);
        }

        public Task<bool> Update(User user, CancellationToken ct)
        {
            return Task.FromResult(true);
        }
    }
}