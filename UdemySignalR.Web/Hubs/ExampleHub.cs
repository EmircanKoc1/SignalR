﻿using Microsoft.AspNetCore.SignalR;

namespace UdemySignalR.Web.Hubs
{
    public class ExampleHub : Hub
    {

        public async Task BroadCastMessageToAllCient(string message)
        {
            await Clients.All.SendAsync("ReceiveMessageForAllClient", message);
        }


    }
}
