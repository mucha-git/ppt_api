namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Views;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class ViewsController : BaseController
{
    private readonly IViewsService _viewsService;
    public ViewsController(IViewsService viewsService)
    {
        _viewsService = viewsService;
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult> GetViews(int id){
        var result = await _viewsService.GetViews(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreateViewRequest request){
        var result = await _viewsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Update(UpdateViewRequest request){
        var result = await _viewsService.Update(request);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> Delete(DeleteViewRequest request){
        await _viewsService.Delete(request.Id);
        return NoContent();
    }

}