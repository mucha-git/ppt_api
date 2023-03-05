namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.MapPins;
using WebApi.Repositories;

public interface IMapPinsService
{
    Task<IEnumerable<MapPins>> GetMapPins(int yearId);
    Task<MapPins> GetMapPinById(int id);
    Task<MapPins> Create(CreateMapPinRequest request);
    Task<MapPins> Update(UpdateMapPinRequest request);
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
}