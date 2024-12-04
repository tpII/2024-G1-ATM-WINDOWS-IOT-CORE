using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ATM.Core.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string message)
        {
            // Difundir el mensaje a todos los clientes conectados
            await Clients.All.SendAsync("NavigateToView", message);
        }
    }
}