using Xunit;
using Moq;
using Serilog;
using FluentAssertions;
using CharityAuction.Application.Services.Logger;

namespace CharityAuction.XUnitTest.Services.Logging;

public class LoggerServiceTests
{
    private readonly Mock<ILogger> _serilogMock;
    private readonly LoggerService _loggerService;

    public LoggerServiceTests()
    {
        _serilogMock = new Mock<ILogger>();
        _loggerService = new LoggerService(_serilogMock.Object);
    }

    [Fact(DisplayName = "Logs informational message via Serilog")]
    public void LogInformation_ValidMessage_CallsSerilogInformation()
    {
        var message = "Info test";

        _loggerService.LogInformation(message);

        _serilogMock.Verify(x => x.Information(message), Times.Once);
    }

    [Fact(DisplayName = "Logs warning message via Serilog")]
    public void LogWarning_ValidMessage_CallsSerilogWarning()
    {
        var message = "Warning test";

        _loggerService.LogWarning(message);

        _serilogMock.Verify(x => x.Warning(message), Times.Once);
    }

    [Fact(DisplayName = "Logs trace message as information via Serilog")]
    public void LogTrace_ValidMessage_CallsSerilogInformation()
    {
        var message = "Trace test";

        _loggerService.LogTrace(message);

        _serilogMock.Verify(x => x.Information(message), Times.Once);
    }

    [Fact(DisplayName = "Logs debug message via Serilog")]
    public void LogDebug_ValidMessage_CallsSerilogDebug()
    {
        var message = "Debug test";

        _loggerService.LogDebug(message);

        _serilogMock.Verify(x => x.Debug(message), Times.Once);
    }

    [Fact(DisplayName = "Logs error with request and message")]
    public void LogError_ValidRequestAndMessage_LogsFormattedError()
    {
        var request = new { Something = "value" };
        var errorMsg = "something went wrong";

        _loggerService.LogError(request, errorMsg);

        _serilogMock.Verify(x =>
            x.Error(It.Is<string>(s =>
                s.Contains("handled with the error:") &&
                s.Contains("something went wrong")
            )),
            Times.Once
        );
    }

    [Fact(DisplayName = "Throws if Serilog logger is null")]
    public void Constructor_NullLogger_ThrowsArgumentNullException()
    {
        Action act = () => new LoggerService(null!);

        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("logger");
    }
}
