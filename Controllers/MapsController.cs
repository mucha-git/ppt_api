namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Models.Maps;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MapsController : BaseController
{
    private readonly IMapsService _mapsService;
    public MapsController(IMapsService mapsService)
    {
        _mapsService = mapsService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetMaps(int id){
        var result = await _mapsService.GetMaps((int)Account.PilgrimageId, id);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("id/{id:int}")]
    public async Task<ActionResult> GetMapById(int id){
        var result = await _mapsService.GetMapById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateMapRequest request){
        var result = await _mapsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateMapRequest request){
        var result = await _mapsService.Update(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        await _mapsService.Delete(id);
        return NoContent();
    }

}