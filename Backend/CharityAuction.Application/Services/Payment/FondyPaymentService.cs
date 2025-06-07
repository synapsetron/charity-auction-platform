using CharityAuction.Application.DTO.Payment;
using CharityAuction.Infrastructure.Options;
using CharityAuction.Payment.Interfaces;
using CloudIpspSDK;
using CloudIpspSDK.Checkout;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

public class FondyPaymentService : IPaymentService
{
    public string ProviderKey => "fondy";
    private readonly FondyPayOptions _options;
    private readonly ILogger<FondyPaymentService> _logger;
    private readonly HttpClient _httpClient;

    public FondyPaymentService(IOptions<FondyPayOptions> options, ILogger<FondyPaymentService> logger, IHttpClientFactory httpClientFactory)
    {
        _options = options.Value;
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();

        Config.MerchantId = _options.MerchantId;
        Config.SecretKey = _options.SecretKey;
    }

    public async Task<string> CreatePaymentAsync(PaymentRequestDTO request)
    {
        try
        {
            string orderId = request.AuctionId.ToString("N");

            var fondyRequest = new CheckoutRequest
            {
                order_id = orderId,
                amount = (int)(request.Amount * 100),
                order_desc = request.Description ?? $"Оплата за аукціон {request.AuctionId}",
                currency = request.Currency ?? "UAH",
                response_url = _options.ResultUrl,
                server_callback_url = _options.ServerUrl
            };

            var response = new Url().Post(fondyRequest);

            if (response.Error != null)
            {
                _logger.LogError("FONDY ERROR: {Error}", JsonConvert.SerializeObject(response.Error));
                return string.Empty;
            }

            _logger.LogInformation("Fondy checkout created: {Url}", response.checkout_url);
            return response.checkout_url;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error via Fondy payment creation");
            return string.Empty;
        }
    }

    public async Task<PaymentStatusResponseDTO> GetPaymentStatusAsync(string paymentId)
    {
        try
        {
            var requestPayload = new
            {
                request = new
                {
                    merchant_id = _options.MerchantId,
                    order_id = paymentId,
                    signature = GetSignature(new Dictionary<string, string>
                    {
                        { "order_id", paymentId },
                        { "merchant_id", _options.MerchantId.ToString() }
                    })
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestPayload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.fondy.eu/api/status/order_id", content);
            var json = await response.Content.ReadAsStringAsync();
            var parsed = JsonConvert.DeserializeObject<dynamic>(json);

            string status = parsed?.response?.order_status ?? "unknown";

            _logger.LogInformation("Fondy status for order {OrderId}: {Status}", paymentId, status);

            return new PaymentStatusResponseDTO
            {
                IsSuccessful = status == "approved",
                Status = status,
                Provider = "Fondy"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка получения статуса платежа через Fondy");
            return new PaymentStatusResponseDTO
            {
                IsSuccessful = false,
                Status = "error",
                Provider = "Fondy"
            };
        }
    }

    public async Task HandleWebhookAsync(string data, string signature)
    {
        try
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            var isValid = VerifySignature(dict, signature);
            if (!isValid)
            {
                _logger.LogWarning("❌ Неверная подпись webhook от Fondy");
                return;
            }

            var payload = JsonConvert.DeserializeObject<FondyCallback>(JsonConvert.SerializeObject(dict));
            _logger.LogInformation("✅ Webhook от Fondy: order_id={OrderId}, status={Status}", payload.order_id, payload.order_status);

            // Твоя логика: обновить заказ в БД и т.п.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Ошибка обработки webhook от Fondy");
        }
    }

    private bool VerifySignature(Dictionary<string, string> fields, string signature)
    {
        var generated = GetSignature(fields);
        _logger.LogInformation("🔐 Calculated: {calc} | Provided: {given}", generated, signature);
        return generated == signature.ToLower();
    }

    private string GetSignature(Dictionary<string, string> fields)
    {
        var filtered = fields
            .Where(kv => kv.Key != "signature")
            .OrderBy(kv => kv.Key)
            .Select(kv => kv.Value ?? "")
            .ToList();

        filtered.Insert(0, _options.SecretKey);
        filtered.Add(_options.SecretKey);

        var raw = string.Join("|", filtered);

        using var sha1 = SHA1.Create();
        var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }


    public class FondyCallback
    {
        public string order_id { get; set; }
        public string order_status { get; set; }
        public string response_status { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string payment_id { get; set; }
    }
}
