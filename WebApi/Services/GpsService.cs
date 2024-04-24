using WebApi.Entities.Traccar;
using WebApi.Repositories;

namespace WebApi.Services;

using AutoMapper;

public interface IGpsService
{
    Task<IEnumerable<Devices>> GetGpsByGroupId(int groupId);
    Task<Devices> GetClientDevicesForApp(int deviceId);
    Task<IEnumerable<Groups>> GetGroups();
}

public class GpsService : IGpsService
{
    private readonly IGpsRepository _gpsRepository;

    public GpsService(IGpsRepository gpsRepository)
    {
        _gpsRepository = gpsRepository;
    }

    public async Task<IEnumerable<Devices>> GetGpsByGroupId(int groupId)
    {
        return await _gpsRepository.GetById(groupId);
    }

    public async Task<Devices> GetClientDevicesForApp(int deviceId)
    {
        return await _gpsRepository.GetClientDevicesFromRedisById(deviceId);
    }

    public async Task<IEnumerable<Groups>> GetGroups()
    {
        return await _gpsRepository.GetGroups();
    }
}