
using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Trade;

namespace WebApi.Factories;

public interface ITradeFactory
{
    Trade Create(TradeRequest tradeRequest, int accountId = 0);
}

public class TradeFactory : BaseFactory, ITradeFactory
{

    public TradeFactory(IMapper mapper) : base(mapper)
    {
    }

    public Trade Create(TradeRequest tradeRequest, int accountId)
    {
        var newTrade = _mapper.Map<Trade>(tradeRequest);
        newTrade.AccountId = accountId;
        return newTrade;
    }
}