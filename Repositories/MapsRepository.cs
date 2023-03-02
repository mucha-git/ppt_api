using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IMapsRepository
{
    Task<IEnumerable<Maps>> Get(int pilgrimageId, int id);

    Task<Maps> GetById(int id);
    Task<Maps> Create(Maps model);
    Task<Maps> Update(Maps model);
    Task Delete(Maps model);
}

public class MapsRepository : IMapsRepository
{
    private readonly DataContext _context;

    public MapsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Maps> Create(Maps model)
    {
        await _context.Maps.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<IEnumerable<Maps>> Get(int pilgrimageId, int id)
    {
        var ret = await _context.Years
            .Where(y => y.PilgrimageId == pilgrimageId && y.Id == id)
            .Include(v => v.Maps).FirstOrDefaultAsync();
        return ret==null? null : ret.Maps;
    }

    public async Task<Maps> Update(Maps model)
    {
        _context.Maps.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task Delete(Maps model){
        _context.Maps.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Maps> GetById(int id)
    {
        return await _context.Maps.Where(e => e.Id == id).Include(m => m.Markers).FirstOrDefaultAsync();
    }
}