namespace SimpleWebAplication.Models;
public class LoginRespose
{
    public string Token { get; set; } = string.Empty;
    public LoginUserInfo? User { get; set; }
}
