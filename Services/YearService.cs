namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Years;
using WebApi.Repositories;

public interface IYearsService
{
    Task<Years> GetData(int yearId);
    Task<IEnumerable<Years>> GetYears(int pilgrimageId);
    Task<Years> Create(CreateYearRequest request);
    Task<Years> Update(UpdateYearRequest request);

    Task Delete(int id);
    Task SaveYearToRedis(int yearId);
}

public class YearsService : IYearsService
{
    private readonly IYearsRepository _yearsRepository;

    private readonly IYearsFactory _yearsFactory;

    private readonly IMapper _mapper;
    
    public YearsService(IYearsRepository yearsRepository, IYearsFactory yearsFactory, IMapper mapper)
    {
        _yearsRepository = yearsRepository;
        _yearsFactory = yearsFactory;
        _mapper = mapper;
    }

    public async Task<Years> GetData(int yearId)
    {
        return await _yearsRepository.GetById(yearId);
    }

    public async Task<IEnumerable<Years>> GetYears(int pilgrimageId)
    {
        return await _yearsRepository.Get(pilgrimageId);
    }

    public async Task<Years> Create(CreateYearRequest request)
    {
        var year = _yearsFactory.Create(request);
        return await _yearsRepository.Create(year);
    }

    public async Task<Years> Update(UpdateYearRequest request)
    {
        var year = _mapper.Map<Years>(request);
        return await _yearsRepository.Update(year);
    }

    public async Task Delete(int id)
    {
        var year = await _yearsRepository.GetById(id);
        await _yearsRepository.Delete(year);
    }

    public async Task SaveYearToRedis(int yearId){
        await _yearsRepository.SaveYearToRedis(yearId);
    }
}