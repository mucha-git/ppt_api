using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IMapPinsRepository
{
    Task<IEnumerable<MapPins>> Get(int yearId);

    Task<MapPins> GetById(int id);
    Task<MapPins> Create(MapPins model);
    Task<MapPins> Update(MapPins model);
    Task Delete(MapPins model);
}

public class MapPinsRepository : IMapPinsRepository
{
    private readonly DataContext _context;

    public MapPinsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<MapPins> Create(MapPins model)
    {
        await _context.MapPins.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<IEnumerable<MapPins>> Get(int yearId)
    {
        var ret = await _context.MapPins
            .Where(y => y.YearId == yearId).ToListAsync();
        return ret;
    }

    public async Task<MapPins> Update(MapPins model)
    {
        _context.MapPins.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task Delete(MapPins model){
        _context.MapPins.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<MapPins> GetById(int id)
    {
        return await _context.MapPins.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
}