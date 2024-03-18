namespace WebApi.Services;

using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models;
using WebApi.Models.MapPins;
using WebApi.Models.Years;
using WebApi.Repositories;

public interface IMapPinsService
{
    Task<IEnumerable<MapPins>> GetMapPins(int yearId);
    Task<MapPins> GetMapPinById(int id);
    Task<MapPins> Create(CreateMapPinRequest request);
    Task<MapPins> Update(UpdateMapPinRequest request);
    Task<List<ChangesResponse>> Copy(CopyMapPinsRequest request);
    Task Delete(int id);
}

public class MapPinsService : IMapPinsService
{
    private readonly IMapPinsRepository _mapPinsRepository;
    private readonly IMapPinsFactory _mapPinsFactory;
    private readonly IMapper _mapper;
    public MapPinsService(IMapPinsRepository mapPinsRepository, IMapPinsFactory mapPinsFactory, IMapper mapper)
    {
        _mapPinsRepository = mapPinsRepository;
        _mapPinsFactory = mapPinsFactory;
        _mapper = mapper;
    }

    public async Task<MapPins> Create(CreateMapPinRequest request)
    {
        var view = _mapPinsFactory.Create(request);
        return await _mapPinsRepository.Create(view);
    }

    public async Task Delete(int id)
    {
        var view = await _mapPinsRepository.GetById(id);
        await _mapPinsRepository.Delete(view);
    }

    public async Task<IEnumerable<MapPins>> GetMapPins(int yearId)
    {
        return await _mapPinsRepository.Get(yearId);
    }

    public async Task<MapPins> GetMapPinById( int id)
    {
        return await _mapPinsRepository.GetById(id);
    }

    public async Task<MapPins> Update(UpdateMapPinRequest request)
    {
        var view = _mapper.Map<MapPins>(request);
        return await _mapPinsRepository.Update(view);
    }

    public async Task<List<ChangesResponse>> Copy(CopyMapPinsRequest request)
    {
        var changes = new List<ChangesResponse>();
        var sourceMapPins = await _mapPinsRepository.Get(request.SourceYearId);
        foreach (var mapPin in sourceMapPins)
        {
            var sourceId = mapPin.Id;
            mapPin.YearId = request.DestinationYearId;
            mapPin.Id = 0;
            //var model = _mapPinsFactory.Create(_mapper.Map<CreateMapPinRequest>(mapPin));
            var newMapPin = await _mapPinsRepository.Create(mapPin);
            changes.Add(new ChangesResponse{ SourceId = sourceId, DestinationId = newMapPin.Id});
        }
        return changes;
    }
}