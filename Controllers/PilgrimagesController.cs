namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.OneSignal;
using WebApi.Models.Pilgrimages;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PilgrimagesController : BaseController
{
    private readonly IPilgrimagesService _pilgrimagesService;
    private readonly IOneSignalService _oneSignalService;
    public PilgrimagesController(IPilgrimagesService pilgrimagesService, IOneSignalService oneSignalService)
    {
        _pilgrimagesService = pilgrimagesService;
        _oneSignalService = oneSignalService;
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

    [HttpPost("oneSignal")]
    [Authorize(Role.Manager, Role.User)]
    public async Task<ActionResult> GetPilgrimages(CreatePostMessage message){
        await _oneSignalService.Push(message, (int)Account.PilgrimageId);
        return Ok();
    }

    [HttpPut]
    [Authorize(Role.Admin, Role.Manager)]
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