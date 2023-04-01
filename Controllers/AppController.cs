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
    private IDistributedCache _cache;
    public AppController(IYearsService yearsService, IDistributedCache cache, IPilgrimagesService pilgrimagesService)
    {
        _yearsService = yearsService;
        _pilgrimagesService = pilgrimagesService;
        _cache = cache;
    }
    
    [HttpPost]
    public async Task<ActionResult> GetYearData(GetYearDataRequest request)
    {
        Years years;
        string recordKey = $"Year_{request.YearId}";
        years = await _cache.GetRecordAsync<Years>(recordKey);
        if (years is null) // Data not available in the Cache
            {
                years = await _yearsService.GetData(request.YearId);
                await _cache.SetRecordAsync(recordKey, years);
            }
        //var result = await _yearsService.GetData(request.PilgrimageId, request.Year);
        return Ok(years);
    }

    [HttpPost("pilgrimage")]
    public async Task<ActionResult> GetPilgrimageData(GetPilgrimageRequest request)
    {
        Pilgrimages pilgrimage;
        string recordKey = $"Pilgrimage_{request.PilgrimageId}";
        pilgrimage = await _cache.GetRecordAsync<Pilgrimages>(recordKey);
        if (pilgrimage is null) // Data not available in the Cache
            {
                var pilgrimages = await _pilgrimagesService.GetPilgrimages(request.PilgrimageId);
                pilgrimage = pilgrimages.FirstOrDefault();
                await _cache.SetRecordAsync(recordKey, pilgrimage);
            }
            
        //var result = await _yearsService.GetData(request.PilgrimageId, request.Year);
        return Ok(pilgrimage);
    }
    
}