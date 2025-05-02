using CharityAuction.Application.DTO.Bid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.DTO.Statistics
{
    public class StatsOverviewDTO
    {
        public string Role { get; set; } = string.Empty;

        // Seller-specific
        public int? MyWins { get; set; }
        public int? MyAuctions { get; set; }
        public decimal? Balance { get; set; }

        // Admin-specific
        public int? UserCount { get; set; }
        public int? AuctionCount { get; set; }
        public int? ActiveAuctionCount { get; set; }
        public int? EndedAuctionCount { get; set; }
        public decimal? TotalBidAmount { get; set; }
        public decimal? AvgBidAmount { get; set; }
        public int? DonationCount { get; set; }
        public IEnumerable<MonthlyBidStatDTO>? MonthlyBids { get; set; }
        public IEnumerable<PopularAuctionStatDTO>? TopAuctions { get; set; }
        public IEnumerable<TopUserStatDTO>? TopUsers { get; set; }

    }
}
