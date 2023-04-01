using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IYearsRepository
{
    Task<IEnumerable<Years>> Get(int pilgrimageId);
    Task<Years> GetById(int year);
    Task<Years> Create(Years model);
    Task<Years> Update(Years model);
    Task Delete(Years model);
    Task SaveYearToRedis(int yearId);
}

public class YearsRepository : IYearsRepository
{
    private readonly DataContext _context;
    private IDistributedCache _cache;
    private IPilgrimagesRepository _pilgrimageRepository;

    public YearsRepository(DataContext context, IDistributedCache cache, IPilgrimagesRepository pilgrimageRepository)
    {
        _context = context;
        _cache = cache;
        _pilgrimageRepository = pilgrimageRepository;
    }

    public async Task<IEnumerable<Years>> Get(int pilgrimageId)
    {
        return await _context.Years.Where(y => y.PilgrimageId == pilgrimageId).OrderBy( o => o.Id).ToListAsync();
    }

    public async Task<Years> GetById(int yearId)
    {
        return await _context.Years
            .Where(y => y.Id == yearId)
            .Include(e => e.Elements.OrderBy(o => o.Order))
            .Include(v => v.Views.OrderBy(o => o.Order))
            .Include(mp => mp.MapPins)
            .Include(m => m.Maps)//.ThenInclude(ma => ma.Markers)
            //.Include(m => m.Maps).ThenInclude(c => c.Polylines.OrderBy(u => u.Id))
            .FirstOrDefaultAsync();
    }

    public async Task<Years> Create(Years model)
    {
        await _context.Years.AddAsync(model);
        await _context.SaveChangesAsync();
        await SaveYearToRedis(model.Id);
        return model;
    }

    public async Task<Years> Update(Years model)
    {
        _context.Years.Update(model);
        await _context.SaveChangesAsync();
        await SaveYearToRedis(model.Id);
        return model;
    }

    public async Task Delete(Years model){
        _context.Years.Remove(model);
        await _context.SaveChangesAsync();
        string recordKey = $"Year_{model.Id}";
        await _cache.RemoveRecordAsync<Years>(recordKey);
        await _pilgrimageRepository.SavePilgrimageToRedis(model.PilgrimageId);
    }

    public async Task SaveYearToRedis(int yearId){
        var year = await GetById(yearId);
        year.Version = Guid.NewGuid();
        await _context.SaveChangesAsync();
        string recordKey = $"Year_{yearId}";
        await _cache.SetRecordAsync(recordKey, year);
        await _pilgrimageRepository.SavePilgrimageToRedis(year.PilgrimageId);
    }
}