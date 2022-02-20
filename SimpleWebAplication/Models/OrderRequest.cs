namespace SimpleWebAplication.Models;
public class OrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0;
}
