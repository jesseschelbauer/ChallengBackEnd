namespace SimpleWebAplication.Models
{
    public class UserPositionResponse
    {
        private decimal _accoutBalance;

        public UserPositionResponse(decimal accoutBalance)
        {
            _accoutBalance = accoutBalance;
        }

        public decimal CheckingAccountAmount { get; set; }
        public List<Position> Positions { get; set; } = new();
        public decimal Consolidated { get; set; }
    }
}
