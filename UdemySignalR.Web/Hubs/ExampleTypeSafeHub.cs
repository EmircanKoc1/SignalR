using Microsoft.AspNetCore.SignalR;

namespace UdemySignalR.Web.Hubs
{
    public class ExampleTypeSafeHub : Hub<IExampleTypeSafeHub>
    {
        private static int ConnectedClientCount = default;

        public async Task BroadCastMessageToAllCient(string message)
        {
            await Clients.All.ReceiveMessageForAllClient(message);

            //await Clients.All.SendAsync("ReceiveMessageForAllClient", message);
        }

        public async Task BroadCastMessageToCallerCient(string message)
        {
            await Clients.Caller.ReceiveMessageForCallerClient(message);

            //await Clients.All.SendAsync("ReceiveMessageForAllClient", message);
        }

        public async Task BroadcastMessageToOthersClient(string message)
        {
            await Clients.Others.ReceiveMessageForOthersClient(message);

            //await Clients.All.SendAsync("ReceiveMessageForAllClient", message);
        }




        public override async Task OnConnectedAsync()
        {
            ConnectedClientCount++;
            await Clients.All.ReceiveConnectedClientCountAllClient(ConnectedClientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedClientCount--;
            await Clients.All.ReceiveConnectedClientCountAllClient(ConnectedClientCount);
            await base.OnDisconnectedAsync(exception);
        }





    }
}
