using BCryptLib = BCrypt.Net.BCrypt;

namespace SimpleWebAplication.Services
{
    public class BCryptService : IBCryptService
    {
        public string HashPassword(string password)
        {
            return BCryptLib.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCryptLib.Verify(password, hash);
        }
    }

    public interface IBCryptService 
    {
        bool Verify(string password, string hash);
        string HashPassword(string password);
    }
}