using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Models.Trade;
using WebApi.SignalR.FrontClientHub;

namespace WebApi.Services
{
    public interface IFrontClientSignalRService
    {
        Task<TradeRequest> UpdateTradeForAccount(int accountId, TradeRequest trade);
        Task<IEnumerable<TradeResponse>> UpdateTradesForAccount(int accountId);
        Task UpdateTradesForAllConnectedAccounts();
        Task UpdateInstrumentsForAllConnectedAccounts();
        Task<ICollection<AccountResponse>> GetAllConnectedUsers();
        List<string> getAllUserConnectionsIds(string userId);
    }

    public class FrontClientSignalRService : IFrontClientSignalRService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ITradeService _tradeService;
        private readonly IHubContext<FrontClientHub> _frontClientHubHub;

        public FrontClientSignalRService(
            DataContext context,
            IMapper mapper,
            IAccountService accountService,
            ITradeService tradeService,
            IHubContext<FrontClientHub> frontClientHubHub
            )
        {
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
            _tradeService = tradeService;
            _frontClientHubHub = frontClientHubHub;
        }

        //SERVER -> CLIENT communication

        public async Task<TradeRequest> UpdateTradeForAccount(int accountId, TradeRequest trade)
        {
            await SendUpdateInformationToClientAndAdmins(trade, accountId, "UpdateTrade");
            return trade;
        }

        public async Task<IEnumerable<TradeResponse>> UpdateTradesForAccount(int accountId)
        {
            var trades = await _tradeService.GetTradesForAccount(accountId);
            if (trades.Count() > 0)
            {
                await SendUpdateInformationToClientAndAdmins(trades, accountId, "UpdateTrades");
                //await SendUpdateInformationToClient(trades, accountId, "UpdateTrades");
            }
            return trades;
        }

        public async Task UpdateTradesForAllConnectedAccounts()
        {
            var accounts = await _accountService.GetAll();
            foreach (var account in accounts)
            {
                if (account.Trades.Count() > 0)
                {
                    await SendUpdateInformationToClient(account.Trades, account.Id, "UpdateTrades");
                }
            }
        }

        public async Task UpdateInstrumentsForAllConnectedAccounts()
        {
            var accounts = await GetAllConnectedUsers();
            foreach (var account in accounts)
            {
                await SendUpdateInformationToClient(TradeController.ActualExchange, account.Id, "UpdateInstruments");
            }
        }

        private async Task SendUpdateInformationToClient(object o, int accountId, string method)
        {
            var userConnections = getAllUserConnectionsIds(accountId.ToString());
            if (userConnections != null)
            {
                await _frontClientHubHub.Clients.Clients(userConnections).SendAsync(method, o);
            }
        }

        private async Task SendUpdateInformationToClientAndAdmins(object o, int accountId, string method)
        {
            var userAsList = await _context.Accounts.Where(a => a.Id == accountId).Select(a => a.Id)
                .ToListAsync();
            var admins = await _context.Accounts
                .Where(a => a.Role == Role.Admin)
                .Select(a => a.Id).ToListAsync();

            userAsList.AddRange(admins);

            foreach (var userId in userAsList)
            {
                var userConnections = getAllUserConnectionsIds(userId.ToString());
                if (userConnections != null)
                    await _frontClientHubHub.Clients.Clients(userConnections).SendAsync(method, o);
            }
        }

        public async Task<ICollection<AccountResponse>> GetAllConnectedUsers()
        {
            var allConnectedUsers = FrontClientHub.ConnectedUsers.Select(kvp => kvp.Key).ToList();
            var users = await _context.Accounts.Where(a => allConnectedUsers.Contains(a.Id.ToString())).ToListAsync();
            var response = _mapper.Map<ICollection<AccountResponse>>(users);
            return response;
        }

        public List<string> getAllUserConnectionsIds(string userId)
        {
            return FrontClientHub.ConnectedUsers.FirstOrDefault(kvp => kvp.Key.Equals(userId)).Value;
        }

        public string getUserIdByConnectionId(string connectionId)
        {
            return FrontClientHub.ConnectedUsers.FirstOrDefault(kvp => kvp.Value.Contains(connectionId)).Key;
        }

        // helper methods
    }
}