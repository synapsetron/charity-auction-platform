namespace CharityAuction.Application.DTO.Auction
{
    public class CreateAuctionRequestDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal StartingPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
