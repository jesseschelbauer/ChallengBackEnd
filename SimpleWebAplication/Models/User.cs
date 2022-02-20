using System.Text.Json.Serialization;
namespace SimpleWebAplication.Models;
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public virtual decimal AccoutBalance { get; private set; } = 0;
    public string Account { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public void IncreaseBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater then zero");
        AccoutBalance += amount;
    }

    public void DecreaseBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater then zero");
        if (amount > AccoutBalance)
            throw new ArgumentException("No limit");

        AccoutBalance -= amount;
    }
}