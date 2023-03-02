namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Repositories;

public interface IYearsService
{
    Task<Years> GetData(int yearId);
    Task SaveYearToRedis(int yearId);
}

public class YearsService : IYearsService
{
    private readonly IYearsRepository _yearsRepository;
    public YearsService(IYearsRepository yearsRepository)
    {
        _yearsRepository = yearsRepository;
    }

    public async Task<Years> GetData(int yearId)
    {
        return await _yearsRepository.Get(yearId);
    }

    public async Task SaveYearToRedis(int yearId){
        await _yearsRepository.SaveYearToRedis(yearId);
    }
}