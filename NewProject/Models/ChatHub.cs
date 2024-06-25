using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NewProject.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string User, string MessageContent)
        {
            await Clients.All.SendAsync("ReceiveMessage", User, MessageContent);
        }

        internal Task SendAsync(string v, object user, string messageContent)
        {
            throw new NotImplementedException();
        }
    }
}
