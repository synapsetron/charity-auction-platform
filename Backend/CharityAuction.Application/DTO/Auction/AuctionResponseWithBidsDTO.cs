
using CharityAuction.Application.DTO.Bid;

namespace CharityAuction.Application.DTO.Auction
{
    public class AuctionResponseWithBidsDTO : AuctionResponseDTO
    {
        public List<BidInfoDTO> Bids { get; set; } = new();
    }
}
