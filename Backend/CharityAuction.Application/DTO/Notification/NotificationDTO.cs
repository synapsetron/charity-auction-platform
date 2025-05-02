using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.DTO.Notification
{
    public class NotificationDTO
    {
        public NotificationType Type { get; set; }

        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string>? Metadata { get; set; }
    }

    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error,
        AuctionWon,
        AuctionLost
    }
}
