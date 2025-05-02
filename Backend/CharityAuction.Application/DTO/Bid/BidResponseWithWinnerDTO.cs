using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.DTO.Bid
{
    public class BidResponseWithWinnerDTO : BidResponseDTO
    {
        public bool isWinner { get; set; } = false;
        public bool isAuctionActive { get; set; } = false;
        public bool isAuctionSold { get; set; } = false;
    }
}
