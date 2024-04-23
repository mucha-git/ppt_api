using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Entities.Traccar;
using WebApi.Helpers;
using WebApi.Models.Years;

namespace WebApi.Repositories;

public interface IGpsRepository
{
    Task<IEnumerable<Devices>> GetById(int groupId);
    Task SaveClientDevicesToRedisById(int groupId);
    Task<IEnumerable<Devices>> GetClientDevicesFromRedisById(int clientId);
    Task<IEnumerable<Groups>> GetGroups();
}

public class GpsRepository : IGpsRepository
{
    private readonly TraccarDataContext _traccarDataContextontext;
    private IDistributedCache _cache;

    public GpsRepository(TraccarDataContext traccatDataContext, IDistributedCache cache)
    {
        _traccarDataContextontext = traccatDataContext;
        _cache = cache;
    }
    
    public async Task<IEnumerable<Devices>> GetById(int groupId)
    {
        return await _traccarDataContextontext.Devices.Where(e => e.GroupId == groupId).Include( p => p.Position).ToListAsync();
    }
    
    public async Task SaveClientDevicesToRedisById(int groupId){
        var devicesEnumerable = await GetById(groupId);
        var recordKey = $"ClientDevices_{groupId}";
        await _cache.SetRecordAsync(recordKey, devicesEnumerable);
    }
    
    public async Task<IEnumerable<Devices>> GetClientDevicesFromRedisById(int groupId)
    {
        var recordKey = $"ClientDevices_{groupId}";
        var devicesEnumerable = await _cache.GetRecordAsync<IEnumerable<Devices>>(recordKey);
        return devicesEnumerable;
    }

    public async Task<IEnumerable<Groups>> GetGroups()
    {
        return await _traccarDataContextontext.Groups.ToListAsync();
    }
}