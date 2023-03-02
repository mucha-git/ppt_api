namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class YearsController : BaseController
{
    private readonly IYearsService _yearsService;
    public YearsController(IYearsService yearsService)
    {
        _yearsService = yearsService;
    }

    /*[HttpGet("{id:int}")]
    public async Task<ActionResult> GetYears(int id){
        var result = await _yearsService.GetYears((int)Account.PilgrimageId, id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateViewRequest request){
        var result = await _viewsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateViewRequest request){
        var result = await _viewsService.Update(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        await _viewsService.Delete(id);
        return NoContent();
    }
*/
    [HttpPost("resetYearInRedis")]
    public async Task<ActionResult> ResetYearToRedis(GetYearDataRequest request)
    {
        await _yearsService.SaveYearToRedis(request.YearId);
        return Ok();
    }
}