namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Maps;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IMapsService
{
    Task<IEnumerable<Maps>> GetMaps(int pilgrimageId, int year);
    Task<Maps> GetMapById(int year);
    Task<Maps> Create(CreateMapRequest request);
    Task<Maps> Update(UpdateMapRequest request);

    Task Delete(int id);
}

public class MapsService : IMapsService
{
    private readonly IMapsRepository _mapsRepository;
    private readonly IMapsFactory _mapsFactory;
    private readonly IMapper _mapper;
    public MapsService(IMapsRepository mapsRepository, IMapsFactory mapsFactory, IMapper mapper)
    {
        _mapsRepository = mapsRepository;
        _mapsFactory = mapsFactory;
        _mapper = mapper;
    }

    public async Task<Maps> Create(CreateMapRequest request)
    {
        var view = _mapsFactory.Create(request);
        return await _mapsRepository.Create(view);
    }

    public async Task Delete(int id)
    {
        var view = await _mapsRepository.GetById(id);
        await _mapsRepository.Delete(view);
    }

    public async Task<IEnumerable<Maps>> GetMaps(int pilgrimageId, int year)
    {
        return await _mapsRepository.Get(pilgrimageId, year);
    }

    public async Task<Maps> GetMapById( int id)
    {
        return await _mapsRepository.GetById(id);
    }

    public async Task<Maps> Update(UpdateMapRequest request)
    {
        var view = _mapper.Map<Maps>(request);
        return await _mapsRepository.Update(view);
    }
}