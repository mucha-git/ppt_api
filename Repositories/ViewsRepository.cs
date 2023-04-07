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
        try{
        foreach(var view in await _context.Views.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order >= model.Order).ToListAsync()){
            view.Order = view.Order + 1;
        }
        await _context.Views.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task<IEnumerable<Views>> Get(int pilgrimageId, int id)
    {
        try {
        var ret = await _context.Years
            .Where(y => y.PilgrimageId == pilgrimageId && y.Id == id)
            .Include(v => v.Views.OrderBy(o => o.Order)).FirstOrDefaultAsync();
        return ret==null? null : ret.Views;
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task<Views> Update(Views model)
    {
        try {
        var oldOrder = _context.Views.AsNoTracking().First(v => v.Id == model.Id).Order;
        if(model.Order < oldOrder){
            foreach(var view in await _context.Views.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order >= model.Order && v.Order < oldOrder).ToListAsync()){
                view.Order = view.Order + 1;
            }
        } else if(model.Order > oldOrder){
            foreach(var view in await _context.Views.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order <= model.Order && v.Order > oldOrder).ToListAsync()){
                view.Order = view.Order - 1;
            }
        }
        _context.Views.Update(model);
        await _context.SaveChangesAsync();
        return model;
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task Delete(Views model){
        try {
        _context.Views.Remove(model);
        await _context.SaveChangesAsync();
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task<Views> GetById(int id)
    {
        return await _context.Views.FirstOrDefaultAsync(e => e.Id == id);
    }
}