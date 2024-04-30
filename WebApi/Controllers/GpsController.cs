using WebApi.Entities.Traccar;
using WebApi.Helpers;
using WebApi.Models.Gps;
using WebApi.Services;

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GpsController : BaseController
{
    private readonly IGpsService _gpsService;

    public GpsController(IGpsService gpsService)
    {
        _gpsService = gpsService;
    }

    [HttpGet("devices/{id:int}")]
    public async Task<ActionResult> GetDevicesWithLocationData(int id)
    {
        var result = await _gpsService.GetGpsByGroupId(id);
        return Ok(result);
    }

    [Authorize(Role.Admin)]
    [HttpGet("groups")]
    public async Task<ActionResult> GetGroups()
    {
        var result = await _gpsService.GetGroups();
        return Ok(result);
    }
}