using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CharityAuction.Application.DTO.Payment;
using CharityAuction.Application.Interfaces.Admin;
using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CharityAuction.XUnitTest.Services.Payment
{
    public class LiqPayServiceTests
    {
        private readonly Mock<ILogger<LiqPayService>> _loggerMock;
        private readonly Mock<IAdminService> _adminServiceMock;
        private readonly IOptions<LiqPayOptions> _options;
        private readonly LiqPayService _service;

        public LiqPayServiceTests()
        {
            _loggerMock = new Mock<ILogger<LiqPayService>>();
            _adminServiceMock = new Mock<IAdminService>();

            _options = Options.Create(new LiqPayOptions
            {
                PublicKey = "test_pub",
                PrivateKey = "test_priv",
                ResultUrl = "https://result.url",
                ServerUrl = "https://server.url"
            });

            _service = new LiqPayService(
                _loggerMock.Object,
                _options,
                _adminServiceMock.Object
            );
        }

        [Fact]
        public async Task CreatePaymentAsync_ValidInput_ReturnsHtmlForm()
        {
            // Arrange
            var request = new PaymentRequestDTO
            {
                Amount = 123.45,
                Currency = "UAH",
                Description = "Test Payment",
                AuctionId = Guid.NewGuid()
            };

            // Act
            var result = await _service.CreatePaymentAsync(request);

            // Assert
            result.Should().Contain("<form");
            result.Should().Contain("liqpay.ua");
            result.Should().Contain("data");
            result.Should().Contain("signature");
        }

        [Fact]
        public async Task HandleWebhookAsync_ValidSignature_ValidPayload_MarksAsPaid()
        {
            // Arrange
            var auctionId = Guid.NewGuid();
            var amount = 500;

            var payload = new
            {
                status = "success",
                order_id = auctionId + "-000000",
                amount
            };

            var json = JsonSerializer.Serialize(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var signature = GenerateSignature(data);

            _adminServiceMock.Setup(x => x.MarkAuctionAsPaidAsync(auctionId, amount)).ReturnsAsync(true);

            // Act
            await _service.HandleWebhookAsync(data, signature);

            // Assert
            _adminServiceMock.Verify(x => x.MarkAuctionAsPaidAsync(auctionId, amount), Times.Once);
        }

        [Fact]
        public async Task HandleWebhookAsync_InvalidSignature_ThrowsArgumentException()
        {
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes("{}"));
            var badSignature = "invalid";

            Func<Task> act = async () => await _service.HandleWebhookAsync(data, badSignature);

            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid signature.");
        }

        [Fact]
        public async Task HandleWebhookAsync_InvalidOrderIdFormat_Throws()
        {
            var payload = new
            {
                status = "success",
                order_id = "badformat",
                amount = 100
            };

            var json = JsonSerializer.Serialize(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var sig = GenerateSignature(data);

            Func<Task> act = async () => await _service.HandleWebhookAsync(data, sig);

            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid order_id format.");
        }

        [Fact]
        public async Task HandleWebhookAsync_InvalidGuidInOrderId_Throws()
        {
            var payload = new
            {
                status = "success",
                order_id = "not-a-guid-00000-0000-0000-000000000000-000000",
                amount = 100
            };

            var json = JsonSerializer.Serialize(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var sig = GenerateSignature(data);

            Func<Task> act = async () => await _service.HandleWebhookAsync(data, sig);

            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid auction ID in order_id.");
        }


        [Fact]
        public async Task HandleWebhookAsync_AuctionUpdateFails_Throws()
        {
            var auctionId = Guid.NewGuid();
            var payload = new
            {
                status = "success",
                order_id = auctionId + "-111111",
                amount = 200
            };

            var json = JsonSerializer.Serialize(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var sig = GenerateSignature(data);

            _adminServiceMock.Setup(x => x.MarkAuctionAsPaidAsync(auctionId, 200)).ReturnsAsync(false);

            Func<Task> act = async () => await _service.HandleWebhookAsync(data, sig);

            await act.Should().ThrowAsync<Exception>().WithMessage($"Auction {auctionId} not found or could not be updated.");
        }

        [Fact]
        public async Task HandleWebhookAsync_StatusNotSuccess_Throws()
        {
            var payload = new
            {
                status = "failure",
                order_id = Guid.NewGuid() + "-111111",
                amount = 200
            };

            var json = JsonSerializer.Serialize(payload);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var sig = GenerateSignature(data);

            Func<Task> act = async () => await _service.HandleWebhookAsync(data, sig);

            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Payment not successful or payload invalid.");
        }

        private string GenerateSignature(string data)
        {
            using var sha1 = SHA1.Create();
            var raw = _options.Value.PrivateKey + data + _options.Value.PrivateKey;
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(raw));
            return Convert.ToBase64String(hash);
        }
    }
}
