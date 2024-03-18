using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IAccountRepository
{
    Task UpdateTradeList(Account account);
}

public class AccountRepository : IAccountRepository
{
    private readonly DataContext _context;

    public AccountRepository(DataContext context)
    {
        _context = context;
    }

    public async Task UpdateTradeList(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
}