using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces;
using CharityAuction.Payment.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using Xunit;

namespace CharityAuction.API.Tests.Controllers;

public class PaymentControllerTests
{
    private readonly Mock<IPaymentServiceStrategy> _paymentStrategyMock = new();
    private readonly Mock<ILogger<PaymentController>> _loggerMock = new();
    private readonly Mock<IAuctionService> _auctionServiceMock = new();
    private readonly PaymentController _controller;

    public PaymentControllerTests()
    {
        _controller = new PaymentController(
            _paymentStrategyMock.Object,
            _loggerMock.Object,
            _auctionServiceMock.Object
        );
    }

    [Fact]
    public async Task CreateLiqPayPayment_ReturnsHtmlContent()
    {
        var request = new PaymentRequestDTO { AuctionId = Guid.NewGuid() };
        _paymentStrategyMock.Setup(p => p.CreatePaymentAsync("liqpay", request))
            .ReturnsAsync("<form>liqpay</form>");

        var result = await _controller.CreateLiqPayPayment(request);

        var content = result as ContentResult;
        content.Should().NotBeNull();
        content!.ContentType.Should().Be("text/html");
        content.Content.Should().Contain("liqpay");
    }

    [Fact]
    public async Task CreateLiqPayPayment_WhenException_Returns500()
    {
        var request = new PaymentRequestDTO { AuctionId = Guid.NewGuid() };
        _paymentStrategyMock.Setup(p => p.CreatePaymentAsync("liqpay", request))
            .ThrowsAsync(new Exception("fail"));

        var result = await _controller.CreateLiqPayPayment(request);

        var response = result as ObjectResult;
        response!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task GetLiqPayPaymentStatus_ReturnsOk()
    {
        var dto = new PaymentStatusResponseDTO { Status = "success" };
        _paymentStrategyMock.Setup(p => p.GetPaymentStatusAsync("liqpay", "order123"))
            .ReturnsAsync(dto);

        var result = await _controller.GetLiqPayPaymentStatus("order123");

        var ok = result as OkObjectResult;
        ok!.StatusCode.Should().Be(200);
        ok.Value.Should().Be(dto);
    }

    [Fact]
    public async Task GetLiqPayPaymentStatus_WhenException_Returns500()
    {
        _paymentStrategyMock.Setup(p => p.GetPaymentStatusAsync("liqpay", "order123"))
            .ThrowsAsync(new Exception("fail"));

        var result = await _controller.GetLiqPayPaymentStatus("order123");

        var status = result as ObjectResult;
        status!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task Webhook_ReturnsOk()
    {
        _paymentStrategyMock.Setup(p => p.HandleWebhookAsync("liqpay", "data", "sig"))
            .Returns(Task.CompletedTask);

        var result = await _controller.Webhook("data", "sig");

        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task Webhook_ArgumentException_ReturnsBadRequest()
    {
        _paymentStrategyMock.Setup(p => p.HandleWebhookAsync("liqpay", "data", "sig"))
            .ThrowsAsync(new ArgumentException("bad"));

        var result = await _controller.Webhook("data", "sig");

        var br = result as BadRequestObjectResult;
        br!.StatusCode.Should().Be(400);
        br.Value.Should().Be("bad");
    }

    [Fact]
    public async Task Webhook_UnexpectedException_Returns500()
    {
        _paymentStrategyMock.Setup(p => p.HandleWebhookAsync("liqpay", "data", "sig"))
            .ThrowsAsync(new Exception("fail"));

        var result = await _controller.Webhook("data", "sig");

        var internalError = result as ObjectResult;
        internalError!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task CreateFondyTestPayment_ReturnsUrl()
    {
        _paymentStrategyMock.Setup(p => p.CreatePaymentAsync("fondy", It.IsAny<PaymentRequestDTO>()))
            .ReturnsAsync("https://pay");

        var result = await _controller.CreateFondyTestPayment();

        var ok = result as OkObjectResult;
        ok!.StatusCode.Should().Be(200);

        ok.Value.Should().BeEquivalentTo(new { url = "https://pay" });
    }


    [Fact]
    public async Task CreateFondyTestPayment_WhenException_Returns500()
    {
        _paymentStrategyMock.Setup(p => p.CreatePaymentAsync("fondy", It.IsAny<PaymentRequestDTO>()))
            .ThrowsAsync(new Exception());

        var result = await _controller.CreateFondyTestPayment();

        var status = result as ObjectResult;
        status!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task FondyWebhook_ValidForm_ReturnsOk()
    {
        var context = new DefaultHttpContext();
        var form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        {
            { "signature", "sig" },
            { "amount", "100" }
        });
        context.Request.ContentType = "application/x-www-form-urlencoded";
        context.Request.Form = form;
        _controller.ControllerContext.HttpContext = context;

        var result = await _controller.FondyWebhook();

        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task FondyWebhook_ValidJson_ReturnsOk()
    {
        var dict = new Dictionary<string, string> { { "signature", "sig" }, { "amount", "100" } };
        var json = System.Text.Json.JsonSerializer.Serialize(dict);

        var context = new DefaultHttpContext();
        context.Request.ContentType = "application/json";
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));
        _controller.ControllerContext.HttpContext = context;

        var result = await _controller.FondyWebhook();

        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task FondyWebhook_UnsupportedContentType_ReturnsBadRequest()
    {
        var context = new DefaultHttpContext();
        context.Request.ContentType = "text/plain";
        _controller.ControllerContext.HttpContext = context;

        var result = await _controller.FondyWebhook();

        var br = result as BadRequestObjectResult;
        br!.Value.Should().Be("Unsupported content type");
    }

    [Fact]
    public async Task FondyWebhook_MissingSignature_ReturnsBadRequest()
    {
        var dict = new Dictionary<string, string> { { "amount", "100" } };
        var json = System.Text.Json.JsonSerializer.Serialize(dict);

        var context = new DefaultHttpContext();
        context.Request.ContentType = "application/json";
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(json));
        _controller.ControllerContext.HttpContext = context;

        var result = await _controller.FondyWebhook();

        var br = result as BadRequestObjectResult;
        br!.Value.Should().Be("Missing signature");
    }

    [Fact]
    public void FondySuccessRedirect_ReturnsRedirect()
    {
        var result = _controller.FondySuccessRedirect();

        var redirect = result as RedirectResult;
        redirect!.Url.Should().Contain("/payment/success");
    }

    [Fact]
    public void FondyFailRedirect_ReturnsRedirect()
    {
        var result = _controller.FondyFailRedirect();

        var redirect = result as RedirectResult;
        redirect!.Url.Should().Contain("/payment/fail");
    }
}
