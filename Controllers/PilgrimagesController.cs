namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Pilgrimages;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PilgrimagesController : BaseController
{
    private readonly IPilgrimagesService _pilgrimagesService;
    public PilgrimagesController(IPilgrimagesService pilgrimagesService)
    {
        _pilgrimagesService = pilgrimagesService;
    }

    [HttpGet]
    public async Task<ActionResult> GetPilgrimages(){
        var result = await _pilgrimagesService.GetPilgrimages(Account.PilgrimageId);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Role.Admin)]
    public async Task<ActionResult> Create(CreatePilgrimageRequest request){
        var result = await _pilgrimagesService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Role.Admin)]
    public async Task<ActionResult> Update(UpdatePilgrimageRequest request){
        var result = await _pilgrimagesService.Update(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Role.Admin)]
    public async Task<ActionResult> Delete(int id){
        await _pilgrimagesService.Delete(id);
        return NoContent();
    }

}