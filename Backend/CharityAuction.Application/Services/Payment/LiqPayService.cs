using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.Admin;
using CharityAuction.Infrastructure.Options;
using CharityAuction.Payment.Interfaces;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class LiqPayService : IPaymentService
{
    private readonly ILogger<LiqPayService> _logger;
    private readonly LiqPayClient _liqPayClient;
    private readonly LiqPayOptions _settings;
    private readonly IAdminService _adminService;
    public string ProviderKey => "liqpay";

    public LiqPayService(
        ILogger<LiqPayService> logger,
        IOptions<LiqPayOptions> options,
        IAdminService adminService
        )
    {
        _logger = logger;
        _settings = options.Value;
        _adminService = adminService;
        _liqPayClient = new LiqPayClient(_settings.PublicKey, _settings.PrivateKey);
    }

    public async Task<string> CreatePaymentAsync(PaymentRequestDTO request)
    {
        var orderId = $"{request.AuctionId}-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        try
        {
            var payload = new Dictionary<string, object?>
        {
            { "public_key", _settings.PublicKey },
            { "version", 3 },
            { "action", "pay" },
            { "amount", request.Amount },
            { "currency", request.Currency },
            { "description", request.Description },
            { "order_id", orderId },
            { "result_url", _settings.ResultUrl },
            { "server_url", _settings.ServerUrl }
        };

            var json = JsonSerializer.Serialize(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var signature = GenerateSignature(data);

            var form = $@"
            <form method='POST' action='https://www.liqpay.ua/api/3/checkout' accept-charset='utf-8'>
                <input type='hidden' name='data' value='{data}' />
                <input type='hidden' name='signature' value='{signature}' />
                <input type='submit' value='Pay Now' />
            </form>
            <script>document.forms[0].submit();</script>";

            _logger.LogInformation("✅ Payment form generated for order {OrderId}", request.AuctionId);
            return form;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error generating payment form for auction {AuctionId}", request.AuctionId);
            throw;
        }
    }

    private string GenerateSignature(string data)
    {
        using var sha1 = SHA1.Create();
        var raw = _settings.PrivateKey + data + _settings.PrivateKey;
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return Convert.ToBase64String(hash);
    }


    public async Task<PaymentStatusResponseDTO> GetPaymentStatusAsync(string paymentId)
    {
        var liqPayRequest = new LiqPayRequest
        {
            Action = LiqPayRequestAction.Status,
            Version = 3,
            OrderId = paymentId
        };

        try
        {
            var response = await _liqPayClient.RequestAsync("request", liqPayRequest);
            var status = response.Status;

            return new PaymentStatusResponseDTO
            {
                IsSuccessful = status == LiqPayResponseStatus.Success,
                Status = status.ToString(),
                Provider = "LiqPay"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving LiqPay payment status for order {OrderId}", paymentId);
            throw;
        }
    }

    public async Task HandleWebhookAsync(string data, string signature)
    {
        var expectedSignature = GenerateSignature(data);
        if (expectedSignature != signature)
        {
            _logger.LogWarning("❌ Signature mismatch in LiqPay webhook.");
            throw new ArgumentException("Invalid signature.");
        }

        var json = Encoding.UTF8.GetString(Convert.FromBase64String(data));
        _logger.LogInformation("🔥 LiqPay webhook payload: {json}", json);

        var payload = JsonSerializer.Deserialize<LiqPayWebhookDTO>(json);
        if (payload?.status == "success" && !string.IsNullOrWhiteSpace(payload.order_id))
        {
            var orderIdParts = payload.order_id.Split('-');
            if (orderIdParts.Length < 6)
                throw new ArgumentException("Invalid order_id format.");

            var guidStr = string.Join("-", orderIdParts.Take(5));
            if (!Guid.TryParse(guidStr, out Guid auctionId))
                throw new ArgumentException("Invalid auction ID in order_id.");

            var updated = await _adminService.MarkAuctionAsPaidAsync(auctionId, payload.amount);
            if (!updated)
                throw new Exception($"Auction {auctionId} not found or could not be updated.");

            _logger.LogInformation("✅ Auction {AuctionId} marked as paid.", auctionId);
        }
        else
        {
            throw new ArgumentException("Payment not successful or payload invalid.");
        }
    }

}
