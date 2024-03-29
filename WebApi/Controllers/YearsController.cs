﻿namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Models.Pilgrimages;
using WebApi.Models.Views;
using WebApi.Models.Years;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class YearsController : BaseController
{
    private readonly IYearsService _yearsService;
    public YearsController(IYearsService yearsService)
    {
        _yearsService = yearsService;
    }

    [HttpGet]
    [Authorize(Role.Manager)]
    public async Task<ActionResult> GetYears(){
        var result = await _yearsService.GetYears((int)Account.PilgrimageId);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Role.Manager)]
    public async Task<ActionResult> Create(CreateYearRequest request){
        var result = await _yearsService.Create(request);
        if(request.SourceYearId != null){
            await Copy(new CopyYearRequest{ 
                SourceYearId = (int)request.SourceYearId, 
                DestinationYearId = result.Id, 
                PilgrimageId = (int)Account.PilgrimageId
            });
        }
        return Ok(result);
    }

    [HttpPost("copy")]
    [Authorize]
    public async Task<ActionResult> Copy(CopyYearRequest request){
        var result = await _yearsService.Copy(request.SourceYearId, request.DestinationYearId);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Role.Manager)]
    public async Task<ActionResult> Update(UpdateYearRequest request){
        var result = await _yearsService.Update(request);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize(Role.Manager)]
    public async Task<ActionResult> Delete(DeleteYearRequest request){
        await _yearsService.Delete(request.Id);
        return NoContent();
    }

    [HttpPost("resetYearInRedis")]
    [Authorize]
    public async Task<ActionResult> ResetYearToRedis(GetYearDataRequest request)
    {
        await _yearsService.SaveYearToRedis(request.YearId);
        return Ok();
    }
}