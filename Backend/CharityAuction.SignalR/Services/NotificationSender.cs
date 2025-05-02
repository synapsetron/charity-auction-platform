using CharityAuction.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using CharityAuction.WebAPI.Hubs;

namespace CharityAuction.SignalR.Services
{
    public class NotificationSender : INotificationSender
    {
        private readonly IHubContext<AuctionHub> _hubContext;

        public NotificationSender(IHubContext<AuctionHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(string userId, string title, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
