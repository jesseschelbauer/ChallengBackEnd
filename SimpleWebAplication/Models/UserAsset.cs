namespace SimpleWebAplication.Models
{
    public class UserAsset
    {
        public int UserId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
        public decimal CurrentPrice { get; set; } = 0;
        public int Id { get; internal set; }
    }
}