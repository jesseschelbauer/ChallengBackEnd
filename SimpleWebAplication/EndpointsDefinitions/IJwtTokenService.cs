using SimpleWebAplication.Models;

namespace SimpleWebAplication.EndpointsDefinitions
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}