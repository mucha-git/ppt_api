using System.Runtime.CompilerServices;

namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Helpers;
using WebApi.Models.Trade;
using WebApi.Repositories;

public interface ITradeService
{
    Task UpdateTrades(IEnumerable<TradeRequest> tradesListRequest, int tradeAccountId);
    Task<TradeResponse> UpdateTrade(TradeRequest request);
    Task UpdateTradeByParentId(int tradeId, int accountId, Statuses status);
    Task UpdateTradeValueByParentId(int tradeId, int accountId, double value);
    Task<IEnumerable<TradeResponse>> GetTradesForClient(int tradeAccountId, Statuses status = Statuses.CLOSED);
    Task<IEnumerable<TradeResponse>> GetTradesForAccount(int accountId);
    Task<IEnumerable<TradeResponse>> UpdateStatusesForAllTrades(int accountId, Statuses status, List<Instrument> actualExchange);

}

public class TradeService : ITradeService
{
    private readonly ITradeRepository _tradeRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ITradeFactory _tradeFactory;
    private readonly IMapper _mapper;
    public TradeService(ITradeRepository tradeRepository, IAccountRepository accountRepository, ITradeFactory tradeFactory, IMapper mapper)
    {
        _tradeRepository = tradeRepository;
        _accountRepository = accountRepository;
        _tradeFactory = tradeFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TradeResponse>> GetTradesForAccount(int accountId)
    {
        var trades = await _tradeRepository.GetTradesForAccount(accountId);
        return _mapper.Map<IEnumerable<TradeResponse>>(trades);
    }

    public async Task<IEnumerable<TradeResponse>> GetTradesForClient(int tradeAccountId, Statuses status)
    {
        return await _tradeRepository.GetTradesForClient(tradeAccountId, status);
    }

    public async Task<TradeResponse> UpdateTrade(TradeRequest request)
    {
        Trade trade = await _tradeRepository.GetTrade(request);
        trade.Volume = request.Volume;
        trade.Value = request.Value;
        trade.Status = (int)request.Status;
        trade.Sl = request.Sl;
        trade.Tp = request.Tp;
        var updatedTradeList = await _tradeRepository.Update(new List<Trade>() { trade });
        if (updatedTradeList.Count() != 1) throw new Exception("dupa zbita");
        return _mapper.Map<TradeResponse>(updatedTradeList.First());
    }

    public async Task UpdateTradeByParentId(int tradeId, int accountId, Statuses status)
    {
        var trade = await _tradeRepository.GetTradeByParentId(tradeId, accountId);
        if (trade != null && !(status == Statuses.ON_HOLD && (int)Statuses.CLOSED == trade.Status))
        {
            trade.Status = (int)status;
            await _tradeRepository.Update(new List<Trade>() { trade });
        }
    }

    public async Task UpdateTradeValueByParentId(int tradeId, int accountId, double value)
    {
        var trade = await _tradeRepository.GetTradeByParentId(tradeId, accountId);
        if (trade != null)
        {
            trade.Value = value;
            await _tradeRepository.Update(new List<Trade>() { trade });
        }
    }

    public async Task UpdateTrades(IEnumerable<TradeRequest> tradesListRequest, int tradeAccountId)
    {
        //get account
        var account = await _accountRepository.GetByTradeAccountId(tradeAccountId);
        var oldTrades = account.Trades;
        if (oldTrades == null) return;
        // usuwanie pozycji
        List<Trade> toDelete = new List<Trade>();
        foreach (Trade trade in oldTrades)
        {
            if (trade.ParentId == null && !tradesListRequest.Any(t => t.Id == trade.Id))
            {
                toDelete.Add(trade);
            }
        }
        //aktualizacja istniejacych pozycji
        List<Trade> toUpdate = new List<Trade>();
        List<Trade> toAdd = new List<Trade>();
        foreach (TradeRequest trade in tradesListRequest)
        {
            int id = 0;
            if (!int.TryParse(trade.Comment, out id))
            {
                if (oldTrades.Any(t => t.Id == trade.Id && (t.Sl != trade.Sl || t.Tp != trade.Tp)))
                {
                    Trade updatedTrade = oldTrades.First(t => t.Id == trade.Id);
                    updatedTrade.Tp = trade.Tp;
                    updatedTrade.Sl = trade.Sl;
                    toUpdate.Add(updatedTrade);
                }
                if (!oldTrades.Any(t => t.Id == trade.Id))
                {
                    //dodanie pozycji
                    var addedTrade = _tradeFactory.Create(trade, account.Id);
                    toAdd.Add(addedTrade);
                }
            }
        }
        await _tradeRepository.Delete(toDelete);
        await _tradeRepository.Update(toUpdate);
        await _tradeRepository.Add(toAdd);
    }

    public async Task<IEnumerable<TradeResponse>> UpdateStatusesForAllTrades(int accountId, Statuses status, List<Instrument> actualExchange)
    {
        var trades = await _tradeRepository.GetTradesForAccount(accountId);
        var tradesForUpdate = new List<Trade>();
        foreach (var trade in trades)
        {
            if (trade.ParentId != null)
            {
                if (CanWeChangeStatus(trade, status, actualExchange))
                {
                    trade.Status = (int)status;
                    tradesForUpdate.Add(trade);
                }
            }
        }
        if(tradesForUpdate.Any()) await _tradeRepository.Update(tradesForUpdate);
        var updatedTrades = await _tradeRepository.GetTradesForAccount(accountId);
        return _mapper.Map<IEnumerable<TradeResponse>>(updatedTrades);
    }

    private bool CanWeChangeStatus(Trade trade, Statuses status, List<Instrument> actualExchange)
    {
        Instrument instrument = actualExchange.Find(i => i.Nazwa == trade.Symbol);
        if (instrument != null)
        {
            if (trade.Status == (int)Statuses.ACTIVE)
            {
                if (status == Statuses.ON_HOLD && ((trade.IsBuying && trade.Value <= (double)instrument.Bid) ||
                                                   (!trade.IsBuying && trade.Value >= (double)instrument.Ask))) return true;
                if (status == Statuses.CLOSED && ((trade.IsBuying && trade.Value > (double)instrument.Bid) ||
                                                   (!trade.IsBuying && trade.Value < (double)instrument.Ask))) return true;
            }

            else if (trade.Status == (int)Statuses.ON_HOLD)
            {
                if (status != Statuses.ON_HOLD) return true;
            }
            
            else if (trade.Status == (int)Statuses.CLOSED)
            {
                if (status != Statuses.CLOSED) return true;
            }
        }

        return false;
    }
}