namespace SimpleWebAplication.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}