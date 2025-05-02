
namespace CharityAuction.Application.DTO.Bid
{
    public class BidResponseDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid AuctionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool isDonated { get; set; } = false;
        public string AuctionName { get; set; } = string.Empty;
    }
}
    