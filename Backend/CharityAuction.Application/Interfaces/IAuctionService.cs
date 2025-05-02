using CharityAuction.Application.DTO.Auction;

namespace CharityAuction.Application.Interfaces
{
    /// <summary>
    /// Defines operations for creating, managing, retrieving, and closing auctions.
    /// </summary>
    public interface IAuctionService
    {
        // ---------------------- Create & Update ----------------------

        /// <summary>
        /// Creates a new auction.
        /// </summary>
        /// <param name="request">The details for the new auction.</param>
        /// <returns>The created <see cref="AuctionResponseDTO"/>.</returns>
        Task<AuctionResponseDTO> CreateAuctionAsync(CreateAuctionRequestDTO request);

        /// <summary>
        /// Updates an existing auction.
        /// </summary>
        /// <param name="request">The updated auction details.</param>
        /// <returns>The updated <see cref="AuctionResponseDTO"/>.</returns>
        Task<AuctionResponseDTO> UpdateAuctionAsync(UpdateAuctionRequestDTO request);

        // ---------------------- Retrieve Auctions ----------------------

        /// <summary>
        /// Retrieves a specific auction by its ID, including all associated bids.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns>The <see cref="AuctionResponseWithBidsDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<AuctionResponseWithBidsDTO?> GetAuctionByIdAsync(Guid auctionId);

        /// <summary>
        /// Retrieves all auctions with their associated bids.
        /// </summary>
        /// <returns>A collection of all <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        Task<IEnumerable<AuctionResponseWithBidsDTO>> GetAllAuctionsAsync();

        /// <summary>
        /// Retrieves all active (currently ongoing) auctions.
        /// </summary>
        /// <returns>A collection of active <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        Task<IEnumerable<AuctionResponseWithBidsDTO>> GetActiveAuctionsAsync();

        /// <summary>
        /// Retrieves all expired auctions that have ended but not processed.
        /// </summary>
        /// <returns>A collection of expired <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        Task<IEnumerable<AuctionResponseWithBidsDTO>> GetExpiredAuctionsAsync();

        /// <summary>
        /// Retrieves all approved auctions that passed moderation.
        /// </summary>
        /// <returns>A collection of approved <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        Task<IEnumerable<AuctionResponseWithBidsDTO>> GetApprovedAuctionsAsync();

        /// <summary>
        /// Retrieves auctions created by the currently logged-in user.
        /// </summary>
        /// <returns>A collection of the user's <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        Task<IEnumerable<AuctionResponseWithBidsDTO>> GetMyAuctionsAsync();

        // ---------------------- Sales & Donations ----------------------

        /// <summary>
        /// Retrieves a list of recent successful auction sales.
        /// </summary>
        /// <returns>A collection of <see cref="RecentSaleDTO"/> representing recent sales.</returns>
        Task<IEnumerable<RecentSaleDTO>> GetRecentSalesAsync();

        /// <summary>
        /// Calculates the total amount donated through sold auctions and donations.
        /// </summary>
        /// <returns>The total donated amount as a <see cref="decimal"/>.</returns>
        Task<decimal> GetTotalDonatedAmountAsync();

        // ---------------------- Auction Closing ----------------------

        /// <summary>
        /// Processes the end of an auction by closing it and determining the winner.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns><c>true</c> if the auction was processed successfully; otherwise, <c>false</c>.</returns>
        Task<bool> ProcessAuctionEndAsync(Guid auctionId);
    }
}
