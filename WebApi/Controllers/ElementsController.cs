namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Elements;
using WebApi.Models.Views;
using WebApi.Services;


[ApiController]
[Route("[controller]")]
public class ElementsController : BaseController
{
    private readonly IElementsService _elementsService;
    public ElementsController(IElementsService elementsService)
    {
        _elementsService = elementsService;
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult> GetElements(int id){
        var result = await _elementsService.GetElements(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Create(CreateElementRequest request){
        var result = await _elementsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> Update(UpdateElementRequest request){
        var result = await _elementsService.Update(request);
        return Ok(result);
    }

    /*[HttpPut("all")]
    //[Authorize]
    public async Task<ActionResult> UpdateAll(){
        var result = await _elementsService.UpdateAll();
        return Ok(result);
    }*/

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> Delete(DeleteElementRequest request){
        await _elementsService.Delete(request.Id);
        return NoContent();
    }

}