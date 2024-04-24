using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.BGServices;

public class GpsBgService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeGpsRepository;
    private readonly IServiceScopeFactory _serviceScopePilgrimagesRepository; 
    public GpsBgService( IServiceScopeFactory serviceScopePilgrimagesRepository, IServiceScopeFactory serviceScopeGpsRepository)
    {
        _serviceScopePilgrimagesRepository = serviceScopePilgrimagesRepository;
        _serviceScopeGpsRepository = serviceScopeGpsRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoServiceStuff();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task DoServiceStuff()
    {
        using var scope = _serviceScopePilgrimagesRepository.CreateScope();
        var pilgrimagesRepository = scope.ServiceProvider.GetRequiredService<IPilgrimagesRepository>(); 
        var pilgrimagesEnumerable = await pilgrimagesRepository.Get(null);
        await IterateOverClientsListToSaveToRedis(pilgrimagesEnumerable);
    }

    private async Task IterateOverClientsListToSaveToRedis(IEnumerable<Pilgrimages> pilgrimagesEnumerable)
    {
        using var scope = _serviceScopeGpsRepository.CreateScope();
        var gpsRepository = scope.ServiceProvider.GetRequiredService<IGpsRepository>(); 
        foreach (var client in pilgrimagesEnumerable)
        {
            client.SetPropsValues();
            if(client.GroupId is not null) await gpsRepository.SaveClientDevicesToRedisById((int)client.GroupId);
        }
    }
}