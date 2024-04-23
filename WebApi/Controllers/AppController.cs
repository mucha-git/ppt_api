using WebApi.Models.Gps;

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Repositories;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class AppController : BaseController
{
    private readonly IYearsService _yearsService;
    private readonly IPilgrimagesService _pilgrimagesService;
    private readonly IGpsService _gpsService;
    public AppController(IYearsService yearsService, IPilgrimagesService pilgrimagesService, IGpsService gpsService)
    {
        _yearsService = yearsService;
        _pilgrimagesService = pilgrimagesService;
        _gpsService = gpsService;
    }
    
    [HttpPost]
    public async Task<ActionResult> GetYearData(GetYearDataRequest request)
    {
        var result = await _yearsService.GetDataForApp(request.YearId);
        return Ok(result);
    }

    [HttpPost("pilgrimage")]
    public async Task<ActionResult> GetPilgrimageData(GetPilgrimageRequest request)
    {
        var pilgrimage = await _pilgrimagesService.GetPilgrimageForApp(request.PilgrimageId);
        return Ok(pilgrimage);
    }
    
    [HttpPost("gps")]
    public async Task<ActionResult> GetGpsData(GetGpsRequest request)
    {
        var clientDevices = await _gpsService.GetClientDevicesForApp(request.GroupId);
        return Ok(clientDevices);
    }
}