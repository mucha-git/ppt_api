using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IViewsRepository
{
    Task<IEnumerable<Views>> Get(int pilgrimageId, int id);

    Task<Views> GetById(int id);
    Task<Views> Create(Views model);
    Task<Views> Update(Views model);
    Task Delete(Views model);
}

public class ViewsRepository : IViewsRepository
{
    private readonly DataContext _context;

    public ViewsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Views> Create(Views model)
    {
        await _context.Views.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<IEnumerable<Views>> Get(int pilgrimageId, int id)
    {
        var ret = await _context.Years
            .Where(y => y.PilgrimageId == pilgrimageId && y.Id == id)
            .Include(v => v.Views).FirstOrDefaultAsync();
        return ret==null? null : ret.Views;
    }

    public async Task<Views> Update(Views model)
    {
        _context.Views.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task Delete(Views model){
        _context.Views.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Views> GetById(int id)
    {
        return await _context.Views.FirstOrDefaultAsync(e => e.Id == id);
    }
}