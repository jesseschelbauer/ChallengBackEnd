using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public abstract class BaseService
    {
        protected readonly IUserInfoService UserInfoService;

        public BaseService(IUserInfoService userInfoService)
        {
            UserInfoService = userInfoService;
        }

        public User User => UserInfoService.User ??  throw new Exception("Invalid user") ;
    }
}
