namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Elements;
using WebApi.Models.Views;
using WebApi.Services;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ElementsController : BaseController
{
    private readonly IElementsService _elementsService;
    public ElementsController(IElementsService elementsService)
    {
        _elementsService = elementsService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetElements(int id){
        var result = await _elementsService.GetElements((int)Account.PilgrimageId, id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateElementRequest request){
        var result = await _elementsService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateElementRequest request){
        var result = await _elementsService.Update(request);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id){
        await _elementsService.Delete(id);
        return NoContent();
    }

}