using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.DTO.Statistics
{
    public class PopularAuctionStatDTO
    {
        public Guid AuctionId { get; set; }
        public string AuctionName { get; set; } = string.Empty;
        public int BidCount { get; set; }
    }
}
