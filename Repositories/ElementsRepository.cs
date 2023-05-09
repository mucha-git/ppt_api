using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IElementsRepository
{
    Task<IEnumerable<Elements>> Get(int pilgrimageId, int id);

    Task<Elements> GetById(int id);
    Task<Elements> Create(Elements model);
    Task<Elements> Update(Elements model);
    Task Delete(Elements model);
}

public class ElementsRepository : IElementsRepository
{
    private readonly DataContext _context;

    public ElementsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Elements> Create(Elements model)
    {
        try {
            foreach(var element in await _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order >= model.Order).ToListAsync()){
                element.Order = element.Order + 1;
            }
            await _context.Elements.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task<IEnumerable<Elements>> Get(int pilgrimageId, int id)
    {
        try{
            var ret = await _context.Years
                .Where(y => y.PilgrimageId == pilgrimageId && y.Id == id)
                .Include(v => v.Elements.OrderBy(o => o.Order)).FirstOrDefaultAsync();
            return ret==null? null : ret.Elements;
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task<Elements> Update(Elements model)
    {
        try{
            var oldOrder = _context.Elements.AsNoTracking().First(v => v.Id == model.Id).Order;
            if(model.Order < oldOrder){
                foreach(var element in await _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order >= model.Order && v.Order < oldOrder).ToListAsync()){
                    element.Order = element.Order + 1;
                }
            } else if(model.Order > oldOrder){
                foreach(var element in await _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order <= model.Order && v.Order > oldOrder).ToListAsync()){
                    element.Order = element.Order - 1;
                }
            }
            
            _context.Elements.Update(model);
            await _context.SaveChangesAsync();
            return model;
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
    }

    public async Task Delete(Elements model){
        try{
            _context.Elements.Remove(model);
        await _context.SaveChangesAsync();
        } catch(Exception e) {
            throw new Exception( e.InnerException.Message);
        }
        
    }

    public async Task<Elements> GetById(int id)
    {
        return await _context.Elements.FirstOrDefaultAsync(e => e.Id == id);
    }
}