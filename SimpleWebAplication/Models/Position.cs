namespace SimpleWebAplication.Models
{
    public class Position
    {
        public decimal Amount { get; set; } = 0;
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; } = 0;
    }
}
