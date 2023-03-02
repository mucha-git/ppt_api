namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Repositories;

public interface IYearsService
{
    Task<Years> GetData(int pilgrimageId, int yearId);
}

public class YearsService : IYearsService
{
    private readonly IYearsRepository _yearsRepository;
    public YearsService(IYearsRepository yearsRepository)
    {
        _yearsRepository = yearsRepository;
    }

    public async Task<Years> GetData(int pilgrimageId, int yearId)
    {
        return await _yearsRepository.Get(pilgrimageId, yearId);
    }
}