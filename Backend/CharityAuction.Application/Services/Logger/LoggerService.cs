using CharityAuction.Application.Interfaces.Logging;
using Serilog;

namespace CharityAuction.Application.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly Serilog.ILogger _logger;

        public LoggerService(Serilog.ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogInformation(string msg) => _logger.Information(msg);
        public void LogWarning(string msg) => _logger.Warning(msg);
        public void LogTrace(string msg) => _logger.Information(msg);
        public void LogDebug(string msg) => _logger.Debug(msg);
        public void LogError(object request, string errorMsg)
        {
            string requestType = request.GetType().ToString();
            string requestClass = requestType.Substring(requestType.LastIndexOf('.') + 1);
            _logger.Error($"{requestClass} handled with the error: {errorMsg}");
        }
    }
}

