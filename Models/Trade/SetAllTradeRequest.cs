namespace WebApi.Models.Trade
{
    public class SetAllTradeRequest
    {
        public IEnumerable<TradeRequest> trades { get; set; }
        public int tradeAccountId { get; set; }
    }
}
