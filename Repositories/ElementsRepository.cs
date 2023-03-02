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
    private readonly IYearsRepository _yearsRepository;

    public ElementsRepository(DataContext context, IYearsRepository yearsRepository)
    {
        _context = context;
        _yearsRepository = yearsRepository;
    }

    public async Task<Elements> Create(Elements model)
    {
        await _context.Elements.AddAsync(model);
        await _context.SaveChangesAsync();
        await _yearsRepository.SaveYearToRedis(model.YearId);
        return model;
    }

    public async Task<IEnumerable<Elements>> Get(int pilgrimageId, int id)
    {
        var ret = await _context.Years
            .Where(y => y.PilgrimageId == pilgrimageId && y.Id == id)
            .Include(v => v.Elements).FirstOrDefaultAsync();
        return ret==null? null : ret.Elements;
    }

    public async Task<Elements> Update(Elements model)
    {
        _context.Elements.Update(model);
        await _context.SaveChangesAsync();
        await _yearsRepository.SaveYearToRedis(model.YearId);
        return model;
    }

    public async Task Delete(Elements model){
        _context.Elements.Remove(model);
        await _context.SaveChangesAsync();
        await _yearsRepository.SaveYearToRedis(model.YearId);
    }

    public async Task<Elements> GetById(int id)
    {
        return await _context.Elements.FirstOrDefaultAsync(e => e.Id == id);
    }
}