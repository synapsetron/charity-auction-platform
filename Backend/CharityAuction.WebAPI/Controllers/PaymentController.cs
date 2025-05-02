using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces;
using CharityAuction.Payment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;
    private readonly IAuctionService _auctionService;

    public PaymentController(IPaymentService paymentService,
        ILogger<PaymentController> logger,
        IAuctionService auctionService)
    {
        _paymentService = paymentService;
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
            var formHtml = await _paymentService.CreatePaymentAsync(request);
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
            var result = await _paymentService.GetPaymentStatusAsync(orderId);
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
        _logger.LogInformation($"Received raw webhook: data={data}, signature={signature}", data, signature);
        try
        {
            await _paymentService.HandleWebhookAsync(data, signature);
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



}
