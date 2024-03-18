using System.Runtime.CompilerServices;
using WebApi.Entities;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Helpers;

public interface IValidations {
 public Task<bool> IsValid(int pilgrimageId, int sourceYearId, int destinationYearId);
 public Task<bool> IsYearValid(int yearId);
 public Task<bool> IsViewValid(int yearId, int viewId);
 public Task<bool> IsElementValid(int elementId);
 public Task<bool> IsMapValid(int mapId);
 public Task<bool> IsMapPinValid(int mapPinId);
}

public class Validations : IValidations
{
    private readonly Account _account;
    private readonly IPilgrimagesRepository _pilgrimagesRepository;
    private readonly IViewsRepository _viewsRepository;
    private readonly IElementsRepository _elementsRepository;
    private readonly IMapsRepository _mapsRepository;
    private readonly IMapPinsRepository _mapPinsRepository;
    public Validations(IAccount account, 
                        IPilgrimagesRepository pilgrimagesRepository,
                        IViewsRepository viewsRepository,
                        IElementsRepository elementsRepository,
                        IMapsRepository mapsRepository,
                        IMapPinsRepository mapPinsRepository){
        _account = account.Account;
        _pilgrimagesRepository = pilgrimagesRepository;
        _viewsRepository = viewsRepository;
        _elementsRepository = elementsRepository;
        _mapsRepository = mapsRepository;
        _mapPinsRepository = mapPinsRepository;
    }
    public async Task<bool> IsValid(int pilgrimageId, int sourceYearId, int destinationYearId)
    {
        var pilgrimage = await _pilgrimagesRepository.GetPilgrimagesFromRedisById(pilgrimageId);
        return _account.PilgrimageId == pilgrimageId 
                && pilgrimage.Years.Select(y => y.Id).Contains(sourceYearId) 
                && pilgrimage.Years.Select(y => y.Id).Contains(destinationYearId);
    }

    public async Task<bool> IsYearValid(int yearId)
    {
        var pilgrimage = await _pilgrimagesRepository.GetPilgrimagesFromRedisById((int)_account.PilgrimageId);
        return  pilgrimage.Years.Select(y => y.Id).Contains(yearId);
    }

    public async Task<bool> IsViewValid(int yearId, int viewId)
    {
        var pilgrimage = await _pilgrimagesRepository.GetPilgrimagesFromRedisById((int)_account.PilgrimageId);
        var view = await _viewsRepository.GetById(viewId);
        return pilgrimage.Years.Select(y => y.Id).Contains(view.YearId) && view.YearId == yearId;
    }

    public async Task<bool> IsElementValid(int elementId)
    {
        var pilgrimage = await _pilgrimagesRepository.GetPilgrimagesFromRedisById((int)_account.PilgrimageId);
        var element = await _elementsRepository.GetById(elementId);
        return pilgrimage.Years.Select(y => y.Id).Contains(element.YearId);
    }

    public async Task<bool> IsMapValid(int mapId)
    {
        var pilgrimage = await _pilgrimagesRepository.GetPilgrimagesFromRedisById((int)_account.PilgrimageId);
        var map = await _mapsRepository.GetById(mapId);
        return pilgrimage.Years.Select(y => y.Id).Contains(map.YearId);
    }

    public async Task<bool> IsMapPinValid(int mapPinId)
    {
        var pilgrimage = await _pilgrimagesRepository.GetPilgrimagesFromRedisById((int)_account.PilgrimageId);
        var mapPin = await _mapPinsRepository.GetById(mapPinId);
        return pilgrimage.Years.Select(y => y.Id).Contains(mapPin.YearId);
    }
}