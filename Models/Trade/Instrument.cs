namespace WebApi.Models.Trade
{
    public class Instrument
    {
        public string Nazwa { get; set; }
        public decimal PipsValue { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public string BidStatus { get; set; }
        public string AskStatus { get; set; }

        public Instrument Update(decimal newBid, decimal newAsk, decimal newPipsValue)
        {
            BidStatus = newBid > Bid ? "primary" : newBid < Bid ? "danger" : BidStatus;
            AskStatus = newAsk > Ask ? "primary" : newAsk < Ask ? "danger" : BidStatus;
            Bid = newBid;
            Ask = newAsk;
            PipsValue = newPipsValue;
            return this;
        }
    }
}
