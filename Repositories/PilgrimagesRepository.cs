using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IPilgrimagesRepository
{
    Task<IEnumerable<Pilgrimages>> Get(int? pilgrimageId);

    Task<Pilgrimages> GetById(int id);
    Task<Pilgrimages> Create(Pilgrimages model);
    Task<Pilgrimages> Update(Pilgrimages model);
    Task Delete(Pilgrimages model);
    Task SavePilgrimageToRedis(int pilgrimageId);
}

public class PilgrimagesRepository : IPilgrimagesRepository
{
    private readonly DataContext _context;
    private IDistributedCache _cache;

    public PilgrimagesRepository(DataContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Pilgrimages> Create(Pilgrimages model)
    {
        await _context.Pilgrimages.AddAsync(model);
        await _context.SaveChangesAsync();
        await SavePilgrimageToRedis(model.Id);
        return model;
    }

    public async Task<IEnumerable<Pilgrimages>> Get(int? pilgrimageId)
    {
        var ret = pilgrimageId != null? 
            await _context.Pilgrimages
            .Where(y => y.Id == pilgrimageId).Include(y => y.Years.OrderBy( o => o.Id)).ToListAsync()
            : await _context.Pilgrimages.Include(y => y.Years.OrderBy( o => o.Id)).ToListAsync();
        return ret;
    }

    public async Task<Pilgrimages> Update(Pilgrimages model)
    {
        _context.Pilgrimages.Update(model);
        await _context.SaveChangesAsync();
        await SavePilgrimageToRedis(model.Id);
        return model;
    }

    public async Task Delete(Pilgrimages model){
        _context.Pilgrimages.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Pilgrimages> GetById(int id)
    {
        return await _context.Pilgrimages.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task SavePilgrimageToRedis(int pilgrimageId){
        var pilgrimage = await Get(pilgrimageId);
        string recordKey = $"Pilgrimage_{pilgrimageId}";
        await _cache.SetRecordAsync(recordKey, pilgrimage.FirstOrDefault());
    }
}