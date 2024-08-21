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


        public async Task BroadcastMessageToIndividualClient(string connectionId, string message)
        {

            await Clients.Client(connectionId).ReceiveMessageForIndividualClient(message);

        }

        public async Task BroadcastMessageToGroupClients(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveMessageForGroupClients(message);

        }

        public async Task AddGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.ReceiveMessageForCallerClient($"gruba dahil oldun : {groupName}");

        }

        public async Task RemoveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Caller.ReceiveMessageForCallerClient($"grubdan ayrıldın : {groupName}");

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
