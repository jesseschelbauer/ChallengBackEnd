using SimpleWebAplication.Models;

public class TransferRequest 
{
    public string Event { get; set; } = "TRANSFER";
    public Target Target { get; set; } = new();
    public Origin Origin { get; set; } = new();
    public decimal Amount { get; set; }
}
