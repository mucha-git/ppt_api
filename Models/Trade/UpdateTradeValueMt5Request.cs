using WebApi.Helpers;

namespace WebApi.Models.Trade
{
    public class UpdateTradeMt5Request
    {
        public int TradeId { get; set; }
        public int AccountId { get; set; }
        public Statuses Status { get; set; }
    }
}
