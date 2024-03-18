namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Models.Maps;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class MapsController : BaseController
{
    private readonly IMapsService _mapsService;
    public MapsController(IMapsService mapsService)
    {
        _mapsService = mapsService;
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult> GetMaps(int id){
        var result = await _mapsService.GetMaps(id);
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("id/{id:int}")]
    public async Task<ActionResult> GetMapById(int id){
        var result = await _mapsService.GetMapById(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreateMapRequest request){
        var result = await _mapsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Update(UpdateMapRequest request){
        var result = await _mapsService.Update(request);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> Delete(DeleteMapRequest request){
        await _mapsService.Delete(request.Id);
        return NoContent();
    }

}