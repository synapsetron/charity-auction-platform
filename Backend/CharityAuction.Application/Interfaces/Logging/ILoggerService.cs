namespace CharityAuction.Application.Interfaces.Logging
{
    /// <summary>
    /// Provides a standard logging service interface for application-wide logging of various events and errors.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="msg">The message to log.</param>
        void LogInformation(string msg);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="msg">The warning message to log.</param>
        void LogWarning(string msg);

        /// <summary>
        /// Logs a trace message, typically used for very detailed debugging information.
        /// </summary>
        /// <param name="msg">The trace message to log.</param>
        void LogTrace(string msg);

        /// <summary>
        /// Logs a debug message, usually used during development and debugging.
        /// </summary>
        /// <param name="msg">The debug message to log.</param>
        void LogDebug(string msg);

        /// <summary>
        /// Logs an error message along with related request data.
        /// </summary>
        /// <param name="request">The request object associated with the error.</param>
        /// <param name="errorMsg">The error message to log.</param>
        void LogError(object request, string errorMsg);
    }
}
