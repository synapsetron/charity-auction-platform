namespace CharityAuction.Application.DTO.Auction
{
    public class AuctionResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal StartingPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public string OrganizerId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }
        public bool isSold { get; set; } = false;

        public bool IsFlagged { get; set; } = false;
        public string? FlaggedReason { get; set; }
    }
}
