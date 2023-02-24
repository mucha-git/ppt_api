namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Views;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ViewsController : BaseController
{
    private readonly IViewsService _viewsService;
    public ViewsController(IViewsService viewsService)
    {
        _viewsService = viewsService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetViews(int id){
        var result = await _viewsService.GetViews((int)Account.PilgrimageId, id);
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

}