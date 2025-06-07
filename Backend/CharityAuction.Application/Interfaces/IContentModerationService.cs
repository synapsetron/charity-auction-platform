using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharityAuction.Application.Interfaces
{
    /// <summary>
    /// Service for detecting harmful or inappropriate content in user input.
    /// </summary>
    public interface IContentModerationService
    {
        /// <summary>
        /// Analyzes a given text and returns true if it is flagged as inappropriate.
        /// </summary>
        /// <param name="text">The text to be analyzed.</param>
        /// <returns>True if the content is flagged, otherwise false.</returns>
        public Task<(bool isFlagged, string? reason)> IsContentFlaggedAsync(string text);
    }
}
