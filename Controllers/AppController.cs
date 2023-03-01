namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class AppController : BaseController
{
    private readonly IYearsService _yearsService;
    private IDistributedCache _cache;
    public AppController(IYearsService yearsService, IDistributedCache cache)
    {
        _yearsService = yearsService;
        _cache = cache;
    }
    
    [HttpPost]
    public async Task<ActionResult> GetAll(GetDataRequest request)
    {
        Years years;
        string recordKey = $"Pilgrimage_{request.PilgrimageId}_Year_{request.Year}";
        years = await _cache.GetRecordAsync<Years>(recordKey);
        if (years is null) // Data not available in the Cache
            {
                years = await _yearsService.GetData(request.PilgrimageId, request.Year);
                await _cache.SetRecordAsync(recordKey, years);
            }
            
        //var result = await _yearsService.GetData(request.PilgrimageId, request.Year);
        return Ok(years);
    }

}