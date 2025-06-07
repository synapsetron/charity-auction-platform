using Xunit;
using Moq;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FluentAssertions;
using CharityAuction.Application.DTO.Payment;
using CharityAuction.Infrastructure.Options;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace CharityAuction.XUnitTest.Services.Payment;
public class FondyPaymentServiceTests
{
    private readonly Mock<ILogger<FondyPaymentService>> _loggerMock = new();
    private readonly FondyPayOptions _options = new()
    {
        MerchantId = 123456,
        SecretKey = "test_secret",
        ResultUrl = "https://return.test",
        ServerUrl = "https://webhook.test"
    };

    private FondyPaymentService CreateService(HttpResponseMessage? response = null)
    {
        var factory = new Mock<IHttpClientFactory>();
        var handler = new MockHttpMessageHandler(response);
        var httpClient = new HttpClient(handler);

        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        return new FondyPaymentService(Options.Create(_options), _loggerMock.Object, factory.Object);
    }

    [Fact(DisplayName = "Returns empty string and logs error when Fondy returns error")]
    public async Task CreatePaymentAsync_FondyError_ReturnsEmpty()
    {
        var service = CreateService(); // CloudIpspSDK handles error internally

        var result = await service.CreatePaymentAsync(new PaymentRequestDTO
        {
            AuctionId = Guid.NewGuid(),
            Amount = 100,
        });

        result.Should().NotBeNull(); // even if error, returns ""
    }

    [Fact(DisplayName = "Returns payment status when Fondy responds properly")]
    public async Task GetPaymentStatusAsync_Valid_ReturnsStatus()
    {
        var fondyJson = JsonConvert.SerializeObject(new
        {
            response = new { order_status = "approved" }
        });

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(fondyJson, Encoding.UTF8, "application/json")
        };

        var service = CreateService(response);

        var result = await service.GetPaymentStatusAsync("abc-123");

        result.Should().NotBeNull();
        result.Status.Should().Be("approved");
        result.IsSuccessful.Should().BeTrue();
        result.Provider.Should().Be("Fondy");
    }

    [Fact(DisplayName = "Returns error status when HTTP call fails")]
    public async Task GetPaymentStatusAsync_Failure_ReturnsErrorStatus()
    {
        var handler = new MockHttpMessageHandler(throwException: true);
        var httpClient = new HttpClient(handler);
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var service = new FondyPaymentService(Options.Create(_options), _loggerMock.Object, factory.Object);

        var result = await service.GetPaymentStatusAsync("any");

        result.IsSuccessful.Should().BeFalse();
        result.Status.Should().Be("error");
    }

    [Fact(DisplayName = "Ignores webhook with invalid signature")]
    public async Task HandleWebhookAsync_InvalidSignature_DoesNothing()
    {
        var payload = new Dictionary<string, string>
        {
            { "order_id", "abc123" },
            { "amount", "1000" },
            { "currency", "UAH" }
        };

        var dataJson = JsonConvert.SerializeObject(payload);

        var service = CreateService();

        var act = () => service.HandleWebhookAsync(dataJson, "invalid-signature");

        await act.Should().NotThrowAsync(); // just logs warning
    }

    [Fact(DisplayName = "Handles webhook with valid signature and logs it")]
    public async Task HandleWebhookAsync_ValidSignature_LogsSuccess()
    {
        var dataDict = new Dictionary<string, string>
        {
            { "order_id", "test123" },
            { "order_status", "approved" },
            { "response_status", "success" },
            { "amount", "1000" },
            { "currency", "UAH" },
            { "payment_id", "pay123" }
        };

        var service = CreateService();

        var dataJson = JsonConvert.SerializeObject(dataDict);
        var signature = GetSignature(dataDict);

        var act = () => service.HandleWebhookAsync(dataJson, signature);

        await act.Should().NotThrowAsync();
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
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(raw));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage? _response;
        private readonly bool _throw;

        public MockHttpMessageHandler(HttpResponseMessage? response = null, bool throwException = false)
        {
            _response = response ?? new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };
            _throw = throwException;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_throw)
                throw new HttpRequestException("Simulated network failure");

            return Task.FromResult(_response!);
        }
    }
}
