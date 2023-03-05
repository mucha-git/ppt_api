namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Models.MapPins;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MapPinsController : BaseController
{
    private readonly IMapPinsService _mapPinsService;
    public MapPinsController(IMapPinsService mapPinsService)
    {
        _mapPinsService = mapPinsService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetMapPins(int id){
        var result = await _mapPinsService.GetMapPins(id);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("id/{id:int}")]
    public async Task<ActionResult> GetMapPinById(int id){
        var result = await _mapPinsService.GetMapPinById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateMapPinRequest request){
        var result = await _mapPinsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateMapPinRequest request){
        var result = await _mapPinsService.Update(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        await _mapPinsService.Delete(id);
        return NoContent();
    }

}