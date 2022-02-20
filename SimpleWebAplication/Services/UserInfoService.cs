using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public class UserInfoService : IUserInfoService 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserInfoService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public User User => _httpContextAccessor.GetUser(); 

        public async Task<decimal> GetAccountBalance(int id, CancellationToken ct) 
        {
            return await _userRepository.GetAccountBalance(id,ct).ConfigureAwait(false);
        }
    }

    public static class IHttpContextAccessorExtensionMethods
    {
        public static User GetUser(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor!.HttpContext!.Items["user"] as User ?? throw new Exception("User info not fond");
        }
    }
}
