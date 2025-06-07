using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces;
using CharityAuction.Payment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentServiceStrategy _paymentStrategy;
    private readonly ILogger<PaymentController> _logger;
    private readonly IAuctionService _auctionService;

    public PaymentController(IPaymentServiceStrategy paymentStrategy,
    ILogger<PaymentController> logger,
    IAuctionService auctionService)
    {
        _paymentStrategy = paymentStrategy;
        _logger = logger;
        _auctionService = auctionService;
    }

    [HttpPost("liqpay")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateLiqPayPayment([FromBody] PaymentRequestDTO request)
    {
        try
        {
            var formHtml = await _paymentStrategy.CreatePaymentAsync("liqpay",request);
            return Content(formHtml, "text/html");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create LiqPay payment for AuctionId {AuctionId}", request.AuctionId);
            return StatusCode(500, "An error occurred while creating the payment.");
        }
    }

    [HttpGet("liqpay/status/{orderId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaymentStatusResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLiqPayPaymentStatus(string orderId)
    {
        try
        {
            var result = await _paymentStrategy.GetPaymentStatusAsync("liqpay", orderId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch LiqPay payment status for OrderId {OrderId}", orderId);
            return StatusCode(500, "An error occurred while retrieving the payment status.");
        }
    }

    [HttpPost("webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> Webhook([FromForm] string data, [FromForm] string signature)
    {
        try
        {
            await _paymentStrategy.HandleWebhookAsync("liqpay",data, signature);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "⚠️ Bad webhook request.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Webhook processing failed.");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("fondy/test")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFondyTestPayment()
    {
        try
        {
            var request = new PaymentRequestDTO
            {
                UserId = "test-user",
                AuctionId = Guid.NewGuid(),
                Amount = 100.00,
                Currency = "UAH",
                Description = "Тестовая оплата через Fondy"
            };

            var url = await _paymentStrategy.CreatePaymentAsync("fondy",request);
            return Ok(new { url });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Ошибка при создании тестового платежа через Fondy");
            return StatusCode(500, "Ошибка при создании платежа");
        }
    }

    [HttpPost("webhook/fondy")]
    [AllowAnonymous]
    public async Task<IActionResult> FondyWebhook()
    {
        try
        {
            Dictionary<string, string> payloadDict;

            if (Request.HasFormContentType)
            {
                var form = await Request.ReadFormAsync();
                payloadDict = form.ToDictionary(x => x.Key, x => x.Value.ToString());
            }
            else if (Request.ContentType?.Contains("application/json") == true)
            {
                using var reader = new StreamReader(Request.Body);
                var body = await reader.ReadToEndAsync();
                payloadDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
            }
            else
            {
                return BadRequest("Unsupported content type");
            }

            if (!payloadDict.TryGetValue("signature", out var signature))
                return BadRequest("Missing signature");

            var dataJson = JsonConvert.SerializeObject(payloadDict);

            _logger.LogInformation("📩 Fondy webhook raw: {Raw}", dataJson);
            await _paymentStrategy.HandleWebhookAsync("fondy", dataJson, signature);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Ошибка обработки webhook Fondy");
            return StatusCode(500);
        }
    }

    [HttpPost("success-redirect")]
    [AllowAnonymous]
    public IActionResult FondySuccessRedirect()
    {
        return Redirect("http://localhost:3000/payment/success");
    }

    [HttpPost("fail-redirect")]
    [AllowAnonymous]
    public IActionResult FondyFailRedirect()
    {
        return Redirect("http://localhost:3000/payment/fail");
    }



}
