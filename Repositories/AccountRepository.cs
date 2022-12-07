using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByTradeAccountId(int tradeAccountId);
    Task UpdateTradeList(Account account);
}

public class AccountRepository : IAccountRepository
{
    private readonly DataContext _context;

    public AccountRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Account> GetByTradeAccountId(int tradeAccountId)
    {
        var account = await _context.Accounts.Where(a => a.TradeAccountId == tradeAccountId).Include(t => t.Trades).FirstOrDefaultAsync();
        return account;
    }

    public async Task UpdateTradeList(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
}