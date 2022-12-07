using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WebApi.Helpers;

namespace WebApi.SignalR.FrontClientHub
{
    public class FrontClientHub : BaseHub
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FrontClientHub(
            DataContext context,
            IMapper mapper
            ) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //CLIENT -> SERVER -> CLIENT communication
        public async Task UpdateNumerZamowieniaForActiveUser(string userId, string zamowienieJSON)
        {
            var userConnections = getAllUserConnectionsIds(userId);
            await Clients.Clients(userConnections).SendAsync("UpdateZamowienia", zamowienieJSON);
        }

        public async Task UpdateNumerAwizacjiForActiveUser(string userId, string awizacjaJSON)
        {
            var userConnections = getAllUserConnectionsIds(userId);
            await Clients.Clients(userConnections).SendAsync("UpdateAwizacji", awizacjaJSON);
        }

    }
}
