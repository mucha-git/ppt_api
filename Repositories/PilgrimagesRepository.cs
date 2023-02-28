using Microsoft.EntityFrameworkCore;
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
}

public class PilgrimagesRepository : IPilgrimagesRepository
{
    private readonly DataContext _context;

    public PilgrimagesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Pilgrimages> Create(Pilgrimages model)
    {
        await _context.Pilgrimages.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<IEnumerable<Pilgrimages>> Get(int? pilgrimageId)
    {
        var ret = pilgrimageId != null? 
            await _context.Pilgrimages
            .Where(y => y.Id == pilgrimageId).ToListAsync()
            : await _context.Pilgrimages.ToListAsync();
        return ret;
    }

    public async Task<Pilgrimages> Update(Pilgrimages model)
    {
        _context.Pilgrimages.Update(model);
        await _context.SaveChangesAsync();
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
}