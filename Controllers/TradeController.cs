using Org.BouncyCastle.Utilities;

namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Trade;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TradeController : BaseController
{
    public static List<Instrument> ActualExchange = new List<Instrument>();
    //private static IEnumerable<TradeResponse> tradeList { get; set; }
    private readonly ITradeService _tradeService;
    private readonly IFrontClientSignalRService _frontClientSignalRService;

    public TradeController(ITradeService tradeService, IFrontClientSignalRService frontClientSignalRService)
    {
        _tradeService = tradeService;
        _frontClientSignalRService = frontClientSignalRService;
    }

    [HttpGet("getTrades")]
    public async Task<ActionResult<IEnumerable<TradeResponse>>> GetAllTradesForAccount()
    {
        var trades = await _tradeService.GetTradesForAccount(Account.Id);
        return Ok(trades);
    }

    [AllowAnonymous]
    [HttpGet("mt5/{id:int}")]
    public async Task<ActionResult<IEnumerable<TradeResponse>>> GetAllForMt5(int id)
    {
        var trades = await _tradeService.GetTradesForClient(id, Statuses.ON_HOLD);
        var response = trades.Where(t => t.Comment != null && int.TryParse(t.Comment, out int number));
        return Ok(response);
    }

    [HttpGet("front")]
    public async Task<ActionResult<IEnumerable<TradeResponse>>> GetAllForFront(int id)
    {
        var trades = await _tradeService.GetTradesForClient(id);
        return Ok(trades);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> SetAll(SetAllTradeRequest request)
    {
        await _tradeService.UpdateTrades(request.trades, request.tradeAccountId);
        await _frontClientSignalRService.UpdateTradesForAllConnectedAccounts(); //SignalR

        return Ok();
    }

    [HttpPatch("front")]
    public async Task<ActionResult<TradeResponse>> UpdateTradeForFront(TradeRequest trade)
    {
        var updatedTrade = await _tradeService.UpdateTrade(trade);
        var trades = await _tradeService.GetTradesForAccount(Account.Id);
        await _frontClientSignalRService.UpdateTradeForAccount(Account.Id, trade); //SignalR
        return Ok(trades);

    }

    [AllowAnonymous]
    [HttpPatch]
    public async Task<ActionResult> UpdateTradeForMt5(UpdateTradeMt5Request request)
    {
        await _tradeService.UpdateTradeByParentId(request.TradeId, request.AccountId, request.Status);
        await _frontClientSignalRService.UpdateTradesForAllConnectedAccounts();//SignalR
        return Ok();
    }

    [HttpPatch("front/updateAll")]
    public async Task<ActionResult<IEnumerable<TradeResponse>>> UpdateStatusesForAllTradesForFront(UpdateAllStatusesRequest request)
    {
        var updatedTrades = await _tradeService.UpdateStatusesForAllTrades(Account.Id, request.Status, ActualExchange);
        await _frontClientSignalRService.UpdateTradesForAccount(Account.Id); //SignalR
        return Ok(updatedTrades);

    }

    [AllowAnonymous]
    [HttpPatch("value")]
    public async Task<ActionResult> UpdateTradeValueForMt5(UpdateTradeValueMt5Request request)
    {
        await _tradeService.UpdateTradeValueByParentId(request.TradeId, request.AccountId, request.Value);
        await _frontClientSignalRService.UpdateTradesForAllConnectedAccounts();//SignalR
        return Ok();
    }


    [AllowAnonymous]
    [HttpPost("actualExchangeUpdate")]//Zamiast GETA dla MT5
    public async Task<ActionResult> ActualExchangeUpdate(List<Instrument> updateList)
    {
        UpdateActualExchangeList(updateList);
        //ActualExchange = updateList;
        await _frontClientSignalRService.UpdateInstrumentsForAllConnectedAccounts(); //SignalR
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("getActualExchange")]
    public ActionResult<IEnumerable<TradeResponse>> GetActualExchangeForFront()
    {
        /*var Instruments = new List<Instrument> {
            new Instrument { Nazwa = "AUDCAD", Ask = 0.88399M, Bid = 0.88699M },
            new Instrument { Nazwa = "AUDNZD", Ask = 1.14049M, Bid = 1.14249M },
            new Instrument { Nazwa = "NZDCAD", Ask = 0.77339M, Bid = 0.77539M }
        };*/
        return Ok(ActualExchange);
    }

    private void UpdateActualExchangeList(List<Instrument> updateList)
    {
        var newList = new List<Instrument>();
        foreach (var item in updateList)
        {
            var instrument = ActualExchange.Where(i => i.Nazwa == item.Nazwa).FirstOrDefault();
            if (instrument == null)
            {
                newList.Add(item);
            }
            else
            {
                newList.Add(instrument.Update(item.Bid, item.Ask, item.PipsValue));
            }
        }
        ActualExchange = newList;
    }
}