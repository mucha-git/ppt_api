namespace WebApi.Models.Trade
{
    public class UpdateTradeValueMt5Request
    {
        public int TradeId { get; set; }
        public int AccountId { get; set; }
        public double Value { get; set; }
    }
}
