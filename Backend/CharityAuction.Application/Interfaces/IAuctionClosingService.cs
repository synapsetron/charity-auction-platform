namespace CharityAuction.Application.Interfaces
{
    /// <summary>
    /// Interface for managing automatic closure of expired auctions.
    /// Scheduled background jobs use this service to ensure auctions end on time.
    /// </summary>
    public interface IAuctionClosingService
    {
        /// <summary>
        /// Closes all active auctions whose end time has already passed.
        /// This method should be invoked periodically by a background worker like Hangfire.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CloseExpiredAuctionsAsync();
    }
}
