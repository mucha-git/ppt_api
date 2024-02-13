namespace WebApi.Services;

using AutoMapper;
using Microsoft.Owin.Security.Provider;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Elements;
using WebApi.Models.MapPins;
using WebApi.Models.Maps;
using WebApi.Models.Views;
using WebApi.Models.Years;
using WebApi.Repositories;

public interface IYearsService
{
    Task<Years> GetDataForApp(int yearId);
    Task<IEnumerable<Years>> GetYears(int pilgrimageId);
    Task<Years> Create(CreateYearRequest request);
    Task<Years> Update(UpdateYearRequest request);
    Task<Years> Copy(int sourceYearId, int destinationYearId);

    Task Delete(int id);
    Task SaveYearToRedis(int yearId);
}

public class YearsService : IYearsService
{
    private readonly IYearsRepository _yearsRepository;

    private readonly IYearsFactory _yearsFactory;

    private readonly IMapper _mapper;
    private readonly IMapPinsService _mapPinsService;
    private readonly IMapsService _mapsService;
    private readonly IViewsService _viewsService;
    private readonly IElementsService _elementsService;
    
    public YearsService(IYearsRepository yearsRepository, IYearsFactory yearsFactory, IMapper mapper, IMapPinsService mapPinsService, IMapsService mapsService, IViewsService viewsService, IElementsService elementsService)
    {
        _yearsRepository = yearsRepository;
        _yearsFactory = yearsFactory;
        _mapper = mapper;
        _mapPinsService = mapPinsService;
        _mapsService = mapsService;
        _viewsService = viewsService;
        _elementsService = elementsService;
    }

    public async Task<Years> GetDataForApp(int yearId)
    {
        return await _yearsRepository.GetYearFromRedisById(yearId);
    }

    public async Task<IEnumerable<Years>> GetYears(int pilgrimageId)
    {
        return await _yearsRepository.Get(pilgrimageId);
    }

    public async Task<Years> Create(CreateYearRequest request)
    {
        var year = _yearsFactory.Create(request);
        return await _yearsRepository.Create(year);
    }

    public async Task<Years> Update(UpdateYearRequest request)
    {
        var year = _mapper.Map<Years>(request);
        return await _yearsRepository.Update(year);
    }

    public async Task Delete(int id)
    {
        var year = await _yearsRepository.GetById(id);
        await _yearsRepository.Delete(year);
    }

    public async Task SaveYearToRedis(int yearId){
        await _yearsRepository.SaveYearToRedis(yearId);
    }

    public async Task<Years> Copy(int sourceYearId, int destinationYearId)
    {
        var destinationYear = await _yearsRepository.GetById(destinationYearId);
        if(destinationYear.Views.Count() != 0 || destinationYear.MapPins.Count() != 0){
            throw new Exception("Aby skopiować dane z poprzedniego rocznika musisz usunąć wszystkie utworzone Widoki, Elementy, Mapy oraz Piny Map z nowego rocznika");
        }
        // map pins
        var mapPinsChanges = await _mapPinsService.Copy(new CopyMapPinsRequest{SourceYearId = sourceYearId, DestinationYearId = destinationYearId});
        // maps
        var mapsChanges = await _mapsService.Copy(new CopyMapRequest{SourceYearId = sourceYearId, DestinationYearId = destinationYearId, MapPinsChanges = mapPinsChanges});
        // views
        var viewsChanges = await _viewsService.Copy(new CopyViewsRequest{SourceYearId = sourceYearId, DestinationYearId = destinationYearId});
        // elements
        await _elementsService.Copy(new CopyElementsRequest{SourceYearId = sourceYearId, DestinationYearId = destinationYearId, MapsChanges = mapsChanges, ViewsChanges = viewsChanges});
        return new Years();
    }
}