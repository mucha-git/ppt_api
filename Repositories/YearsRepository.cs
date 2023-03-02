using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IYearsRepository
{
    Task<Years> Get(int pilgrimageId, int year);
}

public class YearsRepository : IYearsRepository
{
    private readonly DataContext _context;

    public YearsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Years> Get(int pilgrimageId, int yearId)
    {
        return await _context.Years
            .Where(y => y.PilgrimageId == pilgrimageId && y.Id == yearId)
            .Include(e => e.Elements)
            .Include(v => v.Views)
            .Include(mp => mp.MapPins)
            .Include(m => m.Maps).ThenInclude(ma => ma.Markers)
            //.Include(m => m.Maps).ThenInclude(c => c.Polylines.OrderBy(u => u.Id))
            .FirstOrDefaultAsync();
    }
}