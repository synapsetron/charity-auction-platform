using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Services;
using CharityAuction.Payment.Interfaces;

namespace CharityAuction.XUnitTest.Services.Payment;

public class PaymentServiceStrategyTests
{
    private readonly Mock<IPaymentService> _liqpayMock;
    private readonly Mock<IPaymentService> _fondyMock;
    private readonly IPaymentServiceStrategy _strategy;

    public PaymentServiceStrategyTests()
    {
        _liqpayMock = new Mock<IPaymentService>();
        _liqpayMock.SetupGet(x => x.ProviderKey).Returns("liqpay");

        _fondyMock = new Mock<IPaymentService>();
        _fondyMock.SetupGet(x => x.ProviderKey).Returns("fondy");

        _strategy = new PaymentServiceStrategy(new[] { _liqpayMock.Object, _fondyMock.Object });
    }

    [Fact(DisplayName = "CreatePaymentAsync delegates to correct provider")]
    public async Task CreatePaymentAsync_WithValidProvider_CallsCorrectService()
    {
        var dto = new PaymentRequestDTO
        {
            AuctionId = Guid.NewGuid(),
            Amount = 99,
            Currency = "UAH",
            Description = "Test"
        };

        _liqpayMock.Setup(s => s.CreatePaymentAsync(dto)).ReturnsAsync("liqpay-form");

        var result = await _strategy.CreatePaymentAsync("liqpay", dto);

        result.Should().Be("liqpay-form");
        _liqpayMock.Verify(s => s.CreatePaymentAsync(dto), Times.Once);
        _fondyMock.Verify(s => s.CreatePaymentAsync(It.IsAny<PaymentRequestDTO>()), Times.Never);
    }

    [Fact(DisplayName = "GetPaymentStatusAsync delegates to correct provider")]
    public async Task GetPaymentStatusAsync_WithValidProvider_CallsCorrectService()
    {
        var expected = new PaymentStatusResponseDTO { IsSuccessful = true };
        _fondyMock.Setup(s => s.GetPaymentStatusAsync("abc-123")).ReturnsAsync(expected);

        var result = await _strategy.GetPaymentStatusAsync("fondy", "abc-123");

        result.Should().BeEquivalentTo(expected);
        _fondyMock.Verify(s => s.GetPaymentStatusAsync("abc-123"), Times.Once);
        _liqpayMock.Verify(s => s.GetPaymentStatusAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact(DisplayName = "HandleWebhookAsync delegates to correct provider")]
    public async Task HandleWebhookAsync_WithValidProvider_CallsCorrectService()
    {
        var data = "data-string";
        var signature = "signature-string";

        _liqpayMock.Setup(s => s.HandleWebhookAsync(data, signature)).Returns(Task.CompletedTask);

        await _strategy.HandleWebhookAsync("liqpay", data, signature);

        _liqpayMock.Verify(s => s.HandleWebhookAsync(data, signature), Times.Once);
        _fondyMock.Verify(s => s.HandleWebhookAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact(DisplayName = "Throws NotSupportedException for unknown provider")]
    public async Task AnyMethod_WithUnknownProvider_Throws()
    {
        var dto = new PaymentRequestDTO();

        Func<Task> act1 = async () => await _strategy.CreatePaymentAsync("stripe", dto);
        Func<Task> act2 = async () => await _strategy.GetPaymentStatusAsync("stripe", "id");
        Func<Task> act3 = async () => await _strategy.HandleWebhookAsync("stripe", "data", "sig");

        await act1.Should().ThrowAsync<NotSupportedException>().WithMessage("*stripe*");
        await act2.Should().ThrowAsync<NotSupportedException>().WithMessage("*stripe*");
        await act3.Should().ThrowAsync<NotSupportedException>().WithMessage("*stripe*");
    }

    [Fact(DisplayName = "Throws ArgumentException when provider is null or empty")]
    public async Task AnyMethod_WithEmptyProvider_Throws()
    {
        var dto = new PaymentRequestDTO();

        Func<Task> act1 = async () => await _strategy.CreatePaymentAsync("", dto);
        Func<Task> act2 = async () => await _strategy.GetPaymentStatusAsync(" ", "id");
        Func<Task> act3 = async () => await _strategy.HandleWebhookAsync(null!, "data", "sig");

        await act1.Should().ThrowAsync<ArgumentException>().WithMessage("*provider*");
        await act2.Should().ThrowAsync<ArgumentException>().WithMessage("*provider*");
        await act3.Should().ThrowAsync<ArgumentException>().WithMessage("*provider*");
    }
}
