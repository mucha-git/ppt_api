using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IYearsRepository
{
    Task<Years> Get(int year);
    Task SaveYearToRedis(int yearId);
}

public class YearsRepository : IYearsRepository
{
    private readonly DataContext _context;
    private IDistributedCache _cache;

    public YearsRepository(DataContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Years> Get(int yearId)
    {
        return await _context.Years
            .Where(y => y.Id == yearId)
            .Include(e => e.Elements.OrderBy(o => o.Order))
            .Include(v => v.Views.OrderBy(o => o.Order))
            .Include(mp => mp.MapPins)
            .Include(m => m.Maps).ThenInclude(ma => ma.Markers)
            //.Include(m => m.Maps).ThenInclude(c => c.Polylines.OrderBy(u => u.Id))
            .FirstOrDefaultAsync();
    }

    public async Task SaveYearToRedis(int yearId){
        var year = await Get(yearId);
        year.Version = new Guid();
        await _context.SaveChangesAsync();
        string recordKey = $"Year_{yearId}";
        await _cache.SetRecordAsync(recordKey, year);
    }
}