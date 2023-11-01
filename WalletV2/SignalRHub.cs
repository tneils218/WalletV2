using Microsoft.AspNetCore.SignalR;

namespace WalletV2
{
    public class SignalRHub : Hub
    {
        public async Task SendDataToClient(string message)
        {
            await Clients.All.SendAsync("ReceiveData", message);
        }
    }
}