using Microsoft.AspNetCore.SignalR;

namespace CharityAuction.WebAPI.Hubs
{
    public class AuctionHub : Hub
    {
        public async Task JoinAuctionGroup(string auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, auctionId);
        }

        public async Task LeaveAuctionGroup(string auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionId);
        }

        // Сервер вызывает это при новой ставке
        public async Task BroadcastBid(string auctionId, object bid)
        {
            await Clients.Group(auctionId).SendAsync("ReceiveNewBid", bid);
        }

        public async Task BroadcastClose(string auctionId)
        {
            await Clients.Group(auctionId).SendAsync("AuctionClosed", auctionId);
        }
        public async Task SendNotificationToUser(string userId, object notification)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", notification);
        }
    }
}
