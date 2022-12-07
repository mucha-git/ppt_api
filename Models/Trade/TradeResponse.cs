namespace WebApi.Models.Trade
{
    public class TradeResponse
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public double Volume { get; set; }
        public string Comment { get; set; }
        public bool IsBuying { get; set; }
        public double? Tp { get; set; }
        public double? Sl { get; set; }
        public double Value { get; set; }
        public int? AccountId { get; set; }
        public int Status { get; set; }
    }
}
