using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces;
using CharityAuction.Payment.Interfaces;

namespace CharityAuction.Application.Services
{
    public class PaymentServiceStrategy : IPaymentServiceStrategy
    {
        private readonly Dictionary<string, IPaymentService> _serviceMap;

        public PaymentServiceStrategy(IEnumerable<IPaymentService> services)
        {
            _serviceMap = services.ToDictionary(
                s => s.ProviderKey.ToLowerInvariant(),
                s => s
            );
        }

        private IPaymentService Resolve(string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
                throw new ArgumentException("Provider must be specified.", nameof(provider));

            if (!_serviceMap.TryGetValue(provider.ToLowerInvariant(), out var service))
                throw new NotSupportedException($"Provider '{provider}' is not supported.");

            return service;
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
