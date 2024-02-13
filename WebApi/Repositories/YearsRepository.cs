using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Years;

namespace WebApi.Repositories;

public interface IYearsRepository
{
    Task<IEnumerable<Years>> Get(int pilgrimageId);
    Task<Years> GetById(int yearId);
    Task<Years> Create(Years model);
    Task<Years> Update(Years model);
    Task Delete(Years model);
    Task SaveYearToRedis(int yearId);
    Task<YearsDto> GetYearFromRedisById(int yearId);
}

public class YearsRepository : IYearsRepository
{
    private readonly DataContext _context;
    private IDistributedCache _cache;
    private IPilgrimagesRepository _pilgrimageRepository;
    private readonly IMapper _mapper;

    public YearsRepository(DataContext context, IDistributedCache cache, IPilgrimagesRepository pilgrimageRepository, IMapper mapper)
    {
        _context = context;
        _cache = cache;
        _pilgrimageRepository = pilgrimageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Years>> Get(int pilgrimageId)
    {
        return await _context.Years.Where(y => y.PilgrimageId == pilgrimageId).OrderBy( o => o.Id).ToListAsync();
    }

    public async Task<Years> GetById(int yearId)
    {
        Years year = await _context.Years
            .Where(y => y.Id == yearId).FirstOrDefaultAsync();
            if(year != null){
                year.Elements = await _context.Elements.Where(e => e.YearId == yearId).OrderBy(o => o.Order).ToListAsync();
                year.Views = await _context.Views.Where( w => w.YearId == yearId).OrderBy(o => o.Order).ToListAsync();
                year.MapPins = await _context.MapPins.Where( mp => mp.YearId == yearId).ToListAsync();
                year.Maps = await _context.Maps.Where( m => m.YearId == yearId ).ToListAsync();
                foreach (var element in year.Elements)
                {
                    element.SetPropsValues();
                }
            }

        // Years y2 = await _context.Years
        //     .Where(y => y.Id == yearId)
        //     .Include(e => e.Elements.OrderBy(o => o.Order))
        //     .Include(v => v.Views.OrderBy(o => o.Order))
        //     .Include(mp => mp.MapPins)
        //     .Include(m => m.Maps)//.ThenInclude(ma => ma.Markers)
        //     //.Include(m => m.Maps).ThenInclude(c => c.Polylines.OrderBy(u => u.Id))
        //     .FirstOrDefaultAsync();
        
        return year;
    }

    public async Task<Years> Create(Years model)
    {
        await _context.Years.AddAsync(model);
        if(model.isActive) await ChangeActiveYear(model.Id, model.PilgrimageId);
        await _context.SaveChangesAsync();
        await SaveYearToRedis(model.Id);
        return model;
    }

    public async Task<Years> Update(Years model)
    {
        _context.Years.Update(model);
        if(model.isActive) await ChangeActiveYear(model.Id, model.PilgrimageId);
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

    public async Task<YearsDto> GetYearFromRedisById(int yearId)
    {
        YearsDto yearDto;
        string recordKey = $"Year_{yearId}";
        yearDto = await _cache.GetRecordAsync<YearsDto>(recordKey);
        if (yearDto is null) // Data not available in the Cache
            {
                var year = await GetById(yearId);
                yearDto = _mapper.Map<YearsDto>(year);
                await _cache.SetRecordAsync(recordKey, yearDto);
            }
        return yearDto;
    }

    private async Task ChangeActiveYear(int id, int pilgrimageId){
        var years = await _context.Years.Where(y => y.isActive == true && y.Id != id && y.PilgrimageId == pilgrimageId ).ToListAsync();
            foreach (var year in years)
            {
                year.isActive = false;
            }
    }
}