using CharityAuction.Application.DTO.User;
using CharityAuction.Application.DTO.Auction;

namespace CharityAuction.Application.Interfaces.Admin
{
    /// <summary>
    /// Defines administrative operations related to auctions and users.
    /// </summary>
    public interface IAdminService
    {
        // ---------------------- Auctions ----------------------

        /// <summary>
        /// Retrieves all auctions pending approval.
        /// </summary>
        /// <returns>A collection of pending auctions.</returns>
        Task<IEnumerable<AuctionResponseDTO>> GetPendingAuctionsAsync();

        /// <summary>
        /// Approves a specific auction by its ID.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns><c>true</c> if the auction was approved successfully; otherwise, <c>false</c>.</returns>
        Task<bool> ApproveAuctionAsync(Guid auctionId);

        /// <summary>
        /// Rejects a specific auction with a provided reason.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <param name="rejectionReason">The reason for rejection.</param>
        /// <returns><c>true</c> if the auction was rejected successfully; otherwise, <c>false</c>.</returns>
        Task<bool> RejectAuctionAsync(Guid auctionId, string rejectionReason);

        /// <summary>
        /// Blocks a specific auction, preventing further participation.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns><c>true</c> if the auction was blocked successfully; otherwise, <c>false</c>.</returns>
        Task<bool> BlockAuctionAsync(Guid auctionId);

        /// <summary>
        /// Unblocks a previously blocked auction.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns><c>true</c> if the auction was unblocked successfully; otherwise, <c>false</c>.</returns>
        Task<bool> UnblockAuctionAsync(Guid auctionId);

        /// <summary>
        /// Permanently deletes a specific auction.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns><c>true</c> if the auction was deleted successfully; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAuctionAsync(Guid auctionId);

        /// <summary>
        /// Closes an auction manually and finalizes its results.
        /// </summary>
        /// <param name="auctionId">The unique identifier of the auction.</param>
        /// <returns><c>true</c> if the auction was closed successfully; otherwise, <c>false</c>.</returns>
        Task<bool> CloseAuctionAsync(Guid auctionId);

        /// <summary>
        /// Mark an auction as paid.
        /// </summary>
        /// <returns><c>true</c> if the auction was marked as paid successfully; otherwise, <c>false</c>.</returns></returns>
        Task<bool> MarkAuctionAsPaidAsync(Guid auctionId, decimal? paidAmount);

        // ---------------------- Users ----------------------

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        /// <returns>A collection of users.</returns>
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();

        /// <summary>
        /// Blocks a user account by ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns><c>true</c> if the user was blocked successfully; otherwise, <c>false</c>.</returns>
        Task<bool> BlockUserAsync(string userId);

        /// <summary>
        /// Unblocks a previously blocked user account.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns><c>true</c> if the user was unblocked successfully; otherwise, <c>false</c>.</returns>
        Task<bool> UnblockUserAsync(string userId);

        /// <summary>
        /// Permanently deletes a user account.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns><c>true</c> if the user was deleted successfully; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteUserAsync(string userId);
    }
}
