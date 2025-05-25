using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces;
using CharityAuction.Payment.Interfaces;

namespace CharityAuction.Application.Services
{
    public class PaymentServiceStrategy : IPaymentServiceStrategy
    {
        private readonly IEnumerable<IPaymentService> _services;

        public PaymentServiceStrategy(IEnumerable<IPaymentService> services)
        {
            _services = services;
        }

        private IPaymentService Resolve(string provider)
        {
            return provider.ToLower() switch
            {
                "liqpay" => _services.FirstOrDefault(s => s.GetType().Name.ToLower().Contains("liqpay")),
                "fondy" => _services.FirstOrDefault(s => s.GetType().Name.ToLower().Contains("fondy")),
                "stripe" => _services.FirstOrDefault(s => s.GetType().Name.ToLower().Contains("stripe")),
                _ => throw new NotSupportedException($"Provider '{provider}' is not supported.")
            } ?? throw new InvalidOperationException($"No implementation found for provider '{provider}'");
        }

        public Task<string> CreatePaymentAsync(string provider, PaymentRequestDTO request)
        {
            return Resolve(provider).CreatePaymentAsync(request);
        }

        public Task<PaymentStatusResponseDTO> GetPaymentStatusAsync(string provider, string paymentId)
        {
            return Resolve(provider).GetPaymentStatusAsync(paymentId);
        }

        public Task HandleWebhookAsync(string provider, string data, string signature)
        {
            return Resolve(provider).HandleWebhookAsync(data, signature);
        }
    }
}
