namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models.Accounts;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class AppController : BaseController
{
    private readonly IYearsService _yearsService;
    public AppController(IYearsService yearsService)
    {
        _yearsService = yearsService;
    }
    
    [HttpPost]
    public async Task<ActionResult> GetAll(GetDataRequest request)
    {
        var result = await _yearsService.GetData(request.PilgrimageId, request.Year);
        return Ok(result);
    }

}