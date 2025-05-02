
namespace CharityAuction.Application.DTO.Payment
{
    public class LiqPayWebhookDTO
    {
        public string order_id { get; set; }
        public string status { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
}
