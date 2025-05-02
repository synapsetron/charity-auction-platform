using CharityAuction.Application.DTO.Bid;
using CharityAuction.Domain.Entities;

namespace CharityAuction.Application.Interfaces
{
    /// <summary>
    /// Defines operations related to bidding within the Charity Auction system.
    /// </summary>
    public interface IBidService
    {
        // ---------------------- Bid Placement ----------------------

        /// <summary>
        /// Places a new bid on a specified auction.
        /// </summary>
        /// <param name="request">The bid creation details.</param>
        /// <returns>The created <see cref="BidResponseDTO"/>.</returns>
        Task<BidResponseDTO> PlaceBidAsync(CreateBidRequestDTO request);

        // ---------------------- Bid Retrieval ----------------------

        /// <summary>
        /// Retrieves the highest bid for a specific auction.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns>The highest <see cref="BidResponseDTO"/>, or <c>null</c> if no bids exist.</returns>
        Task<BidResponseDTO?> GetHighestBidAsync(Guid auctionId);

        /// <summary>
        /// Retrieves all bids placed on a specific auction.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns>A collection of <see cref="BidResponseDTO"/>.</returns>
        Task<IEnumerable<BidResponseDTO>> GetAllBidsForAuctionAsync(Guid auctionId);

        /// <summary>
        /// Retrieves all bids placed by a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of <see cref="BidResponseDTO"/> made by the user.</returns>
        Task<IEnumerable<BidResponseDTO>> GetUserBidsByUserIdAsync(string userId);

        /// <summary>
        /// Retrieves all bids placed by the currently authenticated user.
        /// </summary>
        /// <returns>A collection of the user's <see cref="BidResponseDTO"/>.</returns>
        Task<IEnumerable<BidResponseDTO>> GetUserBidsAsync();

        /// <summary>
        /// Retrieves a specific bid by its ID.
        /// </summary>
        /// <param name="bidId">The unique identifier of the bid.</param>
        /// <returns>The <see cref="BidResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<BidResponseDTO?> GetBidByIdAsync(Guid bidId);

        /// <summary>
        /// Retrieves a winning bid for the currently authenticated user.
        /// </summary>
        /// <returns>The <see cref="BidResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<IEnumerable<BidResponseDTO>> GetUserWinningBidsAsync();

        /// <summary>
        /// Retrieves all bids of user
        /// </summary>
        /// <returns>The <see cref="BidResponseDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<IEnumerable<BidResponseWithWinnerDTO>> GetUserBidHistoryAsync();

        /// <summary>
        /// Get monthly statistics of bids placed by the user.
        /// </summary>
        /// <returns>The <see cref="MonthlyBidStatDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<IEnumerable<MonthlyBidStatDTO>> GetMonthlyUserBidStatsAsync();

        /// <summary>
        /// Get monthly statistics of bids placed by the system.
        /// </summary>
        /// <returns>The <see cref="MonthlyBidStatDTO"/> if found; otherwise, <c>null</c>.</returns>
        Task<IEnumerable<MonthlyBidStatDTO>> GetMonthlySystemBidStatsAsync();

        /// <summary>
        /// Get all bids with details.
        /// </summary>
        /// <returns>The <see cref="Bid"/> if found; otherwise, <c>null</c>.</returns>
        Task<IEnumerable<Bid>> GetAllBidsWithDetailsAsync();
        // ---------------------- Donation ----------------------

        /// <summary>
        /// Converts a non-winning bid into a voluntary donation after auction closure.
        /// </summary>
        /// <param name="bidId">The unique identifier of the bid to donate.</param>
        /// <returns><c>true</c> if the donation was successful; otherwise, <c>false</c>.</returns>
        Task<bool> DonateBidAsync(Guid bidId);
    }
}
