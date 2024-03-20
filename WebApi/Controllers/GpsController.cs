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

    [HttpPost]
    public ActionResult SetLocationData(GpsDataRequest request)
    {
        _gpsData = request;
        return Ok(request);
    }
}