using CharityAuction.Application.DTO.Payment;

namespace CharityAuction.Application.Interfaces
{
    public interface IPaymentServiceStrategy
    {
        Task<string> CreatePaymentAsync(string provider, PaymentRequestDTO request);
        Task<PaymentStatusResponseDTO> GetPaymentStatusAsync(string provider, string paymentId);
        Task HandleWebhookAsync(string provider, string data, string signature);
    }
}
