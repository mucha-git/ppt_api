namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models;
using WebApi.Models.Maps;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IMapsService
{
    Task<IEnumerable<Maps>> GetMaps(int yearId);
    Task<Maps> GetMapById(int year);
    Task<Maps> Create(CreateMapRequest request);
    Task<Maps> Update(UpdateMapRequest request);
    Task<List<ChangesResponse>> Copy(CopyMapRequest request);
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
        var map = _mapsFactory.Create(request);
        return await _mapsRepository.Create(map);
    }

    public async Task Delete(int id)
    {
        var map = await _mapsRepository.GetById(id);
        await _mapsRepository.Delete(map);
    }

    public async Task<IEnumerable<Maps>> GetMaps(int yearId)
    {
        return await _mapsRepository.Get(yearId);
    }

    public async Task<Maps> GetMapById(int id)
    {
        return await _mapsRepository.GetById(id);
    }

    public async Task<Maps> Update(UpdateMapRequest request)
    {
        var map = _mapper.Map<Maps>(request);
        return await _mapsRepository.Update(map);
    }

    public async Task<List<ChangesResponse>> Copy(CopyMapRequest request)
    {
        var changes = new List<ChangesResponse>();
        var sourceMaps = await _mapsRepository.Get(request.SourceYearId);
        foreach (var map in sourceMaps)
        {
            var sourceId = map.Id;
            var markers = new List<CreateMarkerRequest>();
            foreach (var marker in map.Markers)
            {
                markers.Add(new CreateMarkerRequest
                {
                    Description = marker.Description,
                    FooterColor = marker.FooterColor,
                    FooterText = marker.FooterText,
                    Id = marker.Id,
                    Latitude = marker.Latitude,
                    Longitude = marker.Longitude,
                    PinId = request.MapPinsChanges.First(p => p.SourceId == marker.PinId).DestinationId,
                    StrokeWidth = marker.StrokeWidth,
                    Title = marker.Title
                });
            }
            var toCreate = _mapsFactory.Create(new CreateMapRequest
            {
                Provider = map.Provider,
                Delta = map.Delta,
                Latitude = map.Latitude,
                Longitude = map.Longitude,
                MapSrc = map.MapSrc,
                Name = map.Name,
                Polylines = map.Polylines,
                StrokeColor = map.StrokeColor,
                StrokeWidth = map.StrokeWidth,
                YearId = request.DestinationYearId,
                Markers = markers
            });

            var newMap = await _mapsRepository.Create(toCreate);
            changes.Add(new ChangesResponse { SourceId = sourceId, DestinationId = newMap.Id });
        }
        return changes;
    }
}