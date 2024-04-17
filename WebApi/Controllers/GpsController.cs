using WebApi.Models.Gps;

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GpsController : BaseController
{
    private static GpsDataRequest _gpsData;

    [HttpGet]
    public ActionResult GetLocationData()
    {
        return Ok(_gpsData);
    }

    [HttpGet("set/{id:int}")]
    public ActionResult SetLocationData(int id)
    {
        _gpsData = new GpsDataRequest{Id = id, Latitude = 1, Longitude = 1};
        return Ok(_gpsData);
    }
}