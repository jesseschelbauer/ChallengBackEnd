using SimpleWebAplication.Models;
using SimpleWebAplication.Repositories;

namespace SimpleWebAplication.Services
{
    public class UserPositionService : IUserPositionService 
    {
        private readonly UserRepository _userRepository;

        public UserPositionService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ServiceResult<UserPositionResponse>> GetCurrent(string userId, CancellationToken ct) 
        {
            var user = await _userRepository.Get(userId, ct).ConfigureAwait(false);

            return new UserPositionResponse(user!.AccoutBalance);
        }
    }
}
