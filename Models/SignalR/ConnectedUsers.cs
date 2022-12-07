using WebApi.Entities;

namespace WebApi.Models.SignalR
{
    public class ConnectedUsers
    {
        public IList<Account> connectedUsers { get; set; }
        public IList<string> connectedAdminsId { get; set; }
    }
}
