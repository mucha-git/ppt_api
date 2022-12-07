using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Text.Json;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Accounts;
using WebApi.Models.SignalR;

namespace WebApi.SignalR
{
    public class BaseHub : Hub
    {
        public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BaseHub(
            DataContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {

            var userId = getUserIdByConnectionId(Context.ConnectionId);

            List<string> existingUserConnectionIds;
            ConnectedUsers.TryGetValue(userId, out existingUserConnectionIds);

            // remove the connection id from the List 
            if (existingUserConnectionIds != null)
            {
                existingUserConnectionIds.Remove(Context.ConnectionId);

                // If there are no connection ids in the List, delete the user from the global cache (ConnectedUsers).
                if (existingUserConnectionIds.Count == 0)
                {
                    // if there are no connections for the user,
                    // just delete the userName key from the ConnectedUsers concurent dictionary
                    //List<string> garbage; // to be collected by the Garbage Collector
                    ConnectedUsers.TryRemove(userId, out List<string> garbage);
                }
            }

            await GetAllConnectedUsers();
            //string garbage2;
            await base.OnDisconnectedAsync(e);

        }

        public async Task AddUserWithConnectionId(string userId) //pairing connection with userId to Dictionary
        {
            List<string> existingUserConnectionIds;
            ConnectedUsers.TryGetValue(userId, out existingUserConnectionIds);
            if (existingUserConnectionIds == null)
            {
                existingUserConnectionIds = new List<string>();
            }
            existingUserConnectionIds.Add(Context.ConnectionId);
            ConnectedUsers.TryAdd(userId, existingUserConnectionIds);
            await Clients.Caller.SendAsync("ConnectionID", Context.ConnectionId);
        }

        public async Task GetAllConnectedUsers()
        {
            var connectedUsers = await GetConnectedUsers();
            var response = _mapper.Map<ICollection<AccountResponse>>(connectedUsers.connectedUsers);

            foreach (var adminId in connectedUsers.connectedAdminsId)
            {
                var userConnections = getAllUserConnectionsIds(adminId);
                if (userConnections != null) await Clients.Clients(userConnections).SendAsync("GetAllConnectedUsers", JsonSerializer.Serialize(response));
            }
        }

        private async Task<ConnectedUsers> GetConnectedUsers()
        {
            var allConnectedUsers = ConnectedUsers.Select(kvp => kvp.Key).ToList();
            var connectedUsers = await _context.Accounts
                .Where(a => allConnectedUsers.Contains(a.Id.ToString()))
                .ToListAsync();
            var connectedAdminsId = connectedUsers.Where(u => u.Role == Role.Admin).Select(u => u.Id.ToString()).ToList();
            return new ConnectedUsers() { connectedUsers = connectedUsers, connectedAdminsId = connectedAdminsId };
        }

        protected List<string> getAllUserConnectionsIds(string userId)
        {
            return ConnectedUsers.FirstOrDefault(kvp => kvp.Key.Equals(userId)).Value;
        }

        protected string getUserIdByConnectionId(string connectionId)
        {
            return ConnectedUsers.FirstOrDefault(kvp => kvp.Value.Contains(connectionId)).Key;
        }

    }
}
