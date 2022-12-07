using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Helpers;
using WebApi.Models.Trade;

namespace WebApi.Repositories;

public interface ITradeRepository
{
    Task<Trade> Add(TradeRequest entity, int tradeAccountId);
    Task Add(IEnumerable<Trade> entities);
    Task Delete(IEnumerable<Trade> entity);
    Task<IEnumerable<Trade>> Update(IEnumerable<Trade> entities);
    Task<IEnumerable<TradeResponse>> GetTradesForClient(int tradeAccountId, Statuses status);
    Task<IEnumerable<Trade>> GetTradesForSignal(int accountId);
    Task<Trade> GetTrade(TradeRequest request);
    Task<Trade> GetTradeByParentId(int parentId, int accountId);
    Task<IEnumerable<Trade>> GetTradesForAccount(int accountId);

}

public class TradeRepository : ITradeRepository
{
    private readonly DataContext _context;
    private readonly ITradeFactory _tradeFactory;
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;

    public TradeRepository(DataContext context, ITradeFactory tradeFactory, IMapper mapper, IAccountRepository accountRepository)
    {
        _context = context;
        _tradeFactory = tradeFactory;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    public async Task<Trade> Add(TradeRequest entity, int tradeAccountId)
    {
        Trade trade = _tradeFactory.Create(entity);
        _context.Accounts.Where(a => a.TradeAccountId == tradeAccountId).Include(t => t.Trades).First().Trades.Add(trade);
        await _context.SaveChangesAsync();
        return trade;
    }

    public async Task Add(IEnumerable<Trade> entities)
    {
        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(IEnumerable<Trade> entities)
    {
        _context.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Trade>> Update(IEnumerable<Trade> entities)
    {
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task<IEnumerable<Trade>> GetTradesForAccount(int accountId)
    {
        var account = await _context.Accounts.Where(a => a.Id == accountId).Include(t => t.Trades).FirstOrDefaultAsync();
        if (account.TradeAccountId == null) return null;
        var trades = account.Trades;
        return trades;
    }

    public async Task<IEnumerable<TradeResponse>> GetTradesForClient(int tradeAccountId, Statuses status)
    {
        var account = await _context.Accounts.Where(a => a.TradeAccountId == tradeAccountId).Include(t => t.Trades).FirstOrDefaultAsync();
        if (account == null) return Enumerable.Empty<TradeResponse>();
        var trades = account.Trades.Where(t => t.Status <= (int)status);
        return _mapper.Map<IEnumerable<TradeResponse>>(trades);
    }

    public async Task<IEnumerable<Trade>> GetTradesForSignal(int tradeAccountId)
    {
        var account = await _context.Accounts.Where(a => a.TradeAccountId == tradeAccountId).Include(t => t.Trades).FirstAsync();
        return account.Trades;
    }

    public async Task<Trade> GetTrade(TradeRequest request)
    {
        return await GetTradeFromContext(request.Id, request.AccountId);
    }

    public async Task<Trade> GetTradeByParentId(int parentId, int tradeAccountId)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.TradeAccountId == tradeAccountId);
        if (account == null) return null;
        var trade = await _context.Trades.FirstOrDefaultAsync(t => t.ParentId == parentId && t.AccountId == account.Id);
        if (trade == null) return null;
        return trade;
    }

    private async Task<Trade> GetTradeFromContext(int tradeId, int accountId)
    {
        var trade = await _context.Trades.FirstOrDefaultAsync(t => t.Id == tradeId && t.AccountId == accountId);
        if (trade == null) throw new Exception("dupa");
        return trade;
    }
}