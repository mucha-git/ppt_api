using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.BGServices;

public class GpsBgService : BackgroundService
{
    private readonly IPilgrimagesRepository _pilgrimagesRepository;
    private readonly IGpsRepository _gpsRepository;

    public GpsBgService(IPilgrimagesRepository pilgrimagesRepository, IGpsRepository gpsRepository)
    {
        _pilgrimagesRepository = pilgrimagesRepository;
        _gpsRepository = gpsRepository;
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
        var pilgrimagesEnumerable = await _pilgrimagesRepository.Get(null);
        await IterateOverClientsListToSaveToRedis(pilgrimagesEnumerable);
    }

    private async Task IterateOverClientsListToSaveToRedis(IEnumerable<Pilgrimages> pilgrimagesEnumerable)
    {
        foreach (var client in pilgrimagesEnumerable)
        {
            await _gpsRepository.SaveClientDevicesToRedisById(client.Id);
        }
    }
}