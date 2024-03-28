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

    [HttpGet("set")]
    public ActionResult SetLocationData(int id)
    {
        GpsDataRequest request = new GpsDataRequest {Id = id, Latitude = 345, Longitude = 567};
        _gpsData = request;
        return Ok(request);
    }
}