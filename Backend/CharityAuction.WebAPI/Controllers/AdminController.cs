using CharityAuction.Application.DTO.Auction;
using CharityAuction.Application.DTO.User;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.Admin;
using CharityAuction.WebAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CharityAuction.API.Controllers
{
    /// <summary>
    /// Controller for performing administrative actions on auctions and users.
    /// Accessible only to users with the Admin role.
    /// </summary>
    [ApiController]
    [Route("api/v1/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly ILogger<AdminController> _logger;
        private readonly IHubContext<AuctionHub> _auctionHub;
        private readonly INotificationService _notificationService;

        public AdminController(
            IAdminService adminService,
            ILogger<AdminController> logger,
            IAuctionService auctionService,
            IBidService bidService,
            IHubContext<AuctionHub> hubContext,
            INotificationService notificationService)
        {
            _adminService = adminService;
            _logger = logger;
            _auctionService = auctionService;
            _bidService = bidService;
            _auctionHub = hubContext;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Retrieves all auctions pending approval.
        /// </summary>
        /// <returns>A list of pending auctions.</returns>
        /// <response code="200">Returns the list of pending auctions</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("auctions/pending")]
        [ProducesResponseType(typeof(IEnumerable<AuctionResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingAuctions()
        {
            _logger.LogInformation("Fetching pending auctions.");
            try
            {
                var auctions = await _adminService.GetPendingAuctionsAsync();
                return Ok(auctions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch pending auctions.");
                return StatusCode(500, new { Message = "An error occurred while fetching pending auctions." });
            }
        }

        /// <summary>
        /// Approves an auction.
        /// </summary>
        /// <param name="auctionId">The unique ID of the auction.</param>
        /// <returns>Status of the operation.</returns>
        /// <response code="200">Auction approved successfully</response>
        /// <response code="404">Auction not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("auctions/{auctionId}/approve")]
        public async Task<IActionResult> ApproveAuction(Guid auctionId)
        {
            _logger.LogInformation("Attempting to approve auction {AuctionId}.", auctionId);
            try
            {
                var success = await _adminService.ApproveAuctionAsync(auctionId);
                if (!success)
                    return NotFound(new { Message = "Auction not found." });

                return Ok(new { Message = "Auction approved successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving auction {AuctionId}.", auctionId);
                return StatusCode(500, new { Message = "An error occurred while approving the auction." });
            }
        }

        /// <summary>
        /// Rejects an auction.
        /// </summary>
        /// <param name="auctionId">The ID of the auction to reject.</param>
        /// <param name="request">The rejection reason.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost("auctions/{auctionId}/reject")]
        public async Task<IActionResult> RejectAuction(Guid auctionId, [FromBody] RejectAuctionRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Reason))
                return BadRequest(new { Message = "Rejection reason must be provided." });

            _logger.LogInformation("Rejecting auction {AuctionId} with reason: {Reason}", auctionId, request.Reason);
            try
            {
                var success = await _adminService.RejectAuctionAsync(auctionId, request.Reason);
                if (!success)
                    return NotFound(new { Message = "Auction not found." });

                return Ok(new { Message = "Auction rejected successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting auction {AuctionId}.", auctionId);
                return StatusCode(500, new { Message = "An error occurred while rejecting the auction." });
            }
        }

        /// <summary>
        /// Blocks an auction.
        /// </summary>
        /// <param name="auctionId">The ID of the auction to block.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost("auctions/{auctionId}/block")]
        public async Task<IActionResult> BlockAuction(Guid auctionId)
        {
            _logger.LogInformation("Blocking auction {AuctionId}.", auctionId);
            try
            {
                var success = await _adminService.BlockAuctionAsync(auctionId);
                if (!success)
                    return NotFound(new { Message = "Auction not found." });

                return Ok(new { Message = "Auction blocked successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error blocking auction {AuctionId}.", auctionId);
                return StatusCode(500, new { Message = "An error occurred while blocking the auction." });
            }
        }

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users.");
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve users.");
                return StatusCode(500, new { Message = "An error occurred while retrieving users." });
            }
        }

        /// <summary>
        /// Blocks a user by ID.
        /// </summary>
        [HttpPut("users/{id}/block")]
        public async Task<IActionResult> BlockUser(string id)
        {
            _logger.LogInformation("Blocking user {UserId}.", id);
            try
            {
                var success = await _adminService.BlockUserAsync(id);
                if (!success)
                    return NotFound(new { Message = "User not found." });

                return Ok(new { Message = "User blocked successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to block user {UserId}.", id);
                return StatusCode(500, new { Message = "An error occurred while blocking the user." });
            }
        }

        /// <summary>
        /// Unblocks a user by ID.
        /// </summary>
        [HttpPut("users/{id}/unblock")]
        public async Task<IActionResult> UnblockUser(string id)
        {
            _logger.LogInformation("Unblocking user {UserId}.", id);
            try
            {
                var success = await _adminService.UnblockUserAsync(id);
                if (!success)
                    return NotFound(new { Message = "User not found." });

                return Ok(new { Message = "User unblocked successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to unblock user {UserId}.", id);
                return StatusCode(500, new { Message = "An error occurred while unblocking the user." });
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation("Deleting user {UserId}.", id);
            try
            {
                var success = await _adminService.DeleteUserAsync(id);
                if (!success)
                    return NotFound(new { Message = "User not found." });

                return Ok(new { Message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete user {UserId}.", id);
                return StatusCode(500, new { Message = "An error occurred while deleting the user." });
            }
        }

        /// <summary>
        /// Delete an auction by ID.
        /// </summary>
        [HttpDelete("{auctionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAuction(Guid auctionId)
        {
            var success = await _adminService.DeleteAuctionAsync(auctionId);

            if (!success)
            {
                _logger.LogWarning("Attempt to delete non-existing auction: {AuctionId}", auctionId);
                return NotFound("Auction not found.");
            }

            return NoContent();
        }

        /// <summary>
        /// Manually close an auction and notify the winning bidder.
        /// </summary>
        [HttpPatch("close/{auctionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseAuction(Guid auctionId)
        {
            try
            {
                var success = await _auctionService.ProcessAuctionEndAsync(auctionId);

                if (!success)
                {
                    _logger.LogWarning("Attempted to close a non-existing or already closed auction: {AuctionId}", auctionId);
                    return NotFound("Auction not found, already closed, or not yet finished.");
                }

                // Get the highest bid after closing the auction
                var highestBid = await _bidService.GetHighestBidAsync(auctionId);

                if (highestBid != null)
                {
                    // Create notification in database
                    var notification = await _notificationService.CreateNotificationAsync(
                        highestBid.UserId,
                        "Congratulations!",
                        $"You have won the auction '{highestBid.AuctionName}' with a bid of {highestBid.Amount:C}."
                    );

                    // Send real-time notification via SignalR
                    await _auctionHub.Clients.User(highestBid.UserId)
                        .SendAsync("ReceiveNotification", new
                        {
                            Title = notification.Title,
                            Message = notification.Message,
                            CreatedAt = notification.CreatedAt
                        });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while closing the auction {AuctionId}.", auctionId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}