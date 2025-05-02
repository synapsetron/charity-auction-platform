namespace CharityAuction.Application.DTO.Bid
{
    public class CreateBidRequestDTO
    {
        public Guid AuctionId { get; set; }
        public decimal Amount { get; set; }
    }

}
