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
    public AppController(IYearsService yearsService, IPilgrimagesService pilgrimagesService)
    {
        _yearsService = yearsService;
        _pilgrimagesService = pilgrimagesService;
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
    
}