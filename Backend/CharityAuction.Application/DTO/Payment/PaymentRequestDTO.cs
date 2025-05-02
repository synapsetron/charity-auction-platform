namespace CharityAuction.Application.DTO.Payment
{
    public class PaymentRequestDTO
    {
        public string UserId { get; set; }
        public Guid AuctionId { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; } = "UAN";
        public string Description { get; set; }
    }
}
