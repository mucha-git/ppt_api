using WebApi.Models.Gps;

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GpsController : BaseController
{
    [HttpPost]
    public ActionResult SetLocationData(GpsDataRequest request)
    {
        return Ok(request);
    }
}