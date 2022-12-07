using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; }

        public static int TotalUsers { get; set; }

        public async Task<int> NewWindowLoaded()
        {
            // send total count to all clients
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);

            return TotalViews;
        }

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("connectedUsersCountChanged", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("connectedUsersCountChanged", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}