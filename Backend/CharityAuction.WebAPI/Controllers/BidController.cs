using CharityAuction.Application.DTO.Bid;
using CharityAuction.Application.Interfaces;
using CharityAuction.Domain.Entities;
using CharityAuction.WebAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CharityAuction.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;
        private readonly ILogger<BidController> _logger;
        private readonly IHubContext<AuctionHub> _auctionHub;

        public BidController(
            IBidService bidService,
            ILogger<BidController> logger,
            IHubContext<AuctionHub> auctionHub

        ){
            _bidService = bidService;
            _auctionHub = auctionHub;
            _logger = logger;
        }

        /// <summary>
        /// Place a new bid on an auction.
        /// </summary>
        /// <param name="request">Bid details</param>
        /// <returns>Created bid</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BidResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PlaceBid([FromBody] CreateBidRequestDTO request)
        {
            try
            {
                var result = await _bidService.PlaceBidAsync(request);
                await _auctionHub.Clients.Group(result.AuctionId.ToString())
            .SendAsync("ReceiveNewBid", result);
                return CreatedAtAction(nameof(GetBidById), new { bidId = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business validation failed while placing bid.");
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized attempt to place bid.");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while placing bid.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Get the highest bid for an auction.
        /// </summary>
        /// <param name="auctionId">Auction identifier</param>
        /// <returns>Highest bid</returns>
        [HttpGet("auction/{auctionId}/highest")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BidResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHighestBid(Guid auctionId)
        {
            var bid = await _bidService.GetHighestBidAsync(auctionId);

            if (bid == null)
            {
                _logger.LogInformation("No bids found for auction {AuctionId}", auctionId);
                return NotFound("No bids found for this auction.");
            }

            return Ok(bid);
        }

        /// <summary>
        /// Get all bids for a specific auction.
        /// </summary>
        /// <param name="auctionId">Auction identifier</param>
        /// <returns>List of bids</returns>
        [HttpGet("auction/{auctionId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<BidResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBidsForAuction(Guid auctionId)
        {
            var bids = await _bidService.GetAllBidsForAuctionAsync(auctionId);

            if (!bids.Any())
            {
                _logger.LogInformation("No bids found for auction {AuctionId}", auctionId);
                return NotFound("No bids found for this auction.");
            }

            return Ok(bids);
        }

        /// <summary>
        /// Get all bids placed by the current authenticated user.
        /// </summary>
        /// <returns>List of user's bids</returns>
        [HttpGet("bid/user")]
        [ProducesResponseType(typeof(IEnumerable<BidResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserBids()
        {
            try
            {

                var bids = await _bidService.GetUserBidsAsync();
                return Ok(bids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching user bids.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Get all bids placed by a specific user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>List of user's bids</returns>
        [HttpGet("by-user")]
        [ProducesResponseType(typeof(IEnumerable<BidResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserBidsByUserId([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("GetUserBidsByUserId called without userId.");
                return BadRequest("User ID is required.");
            }

            try
            {
                var bids = await _bidService.GetUserBidsByUserIdAsync(userId);
                return Ok(bids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching bids by user ID.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        /// <summary>
        /// Get a bid by its ID.
        /// </summary>
        /// <param name="bidId">Bid identifier</param>
        /// <returns>Bid details</returns>
        [HttpGet("{bidId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BidResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBidById(Guid bidId)
        {
            var bid = await _bidService.GetBidByIdAsync(bidId);

            if (bid == null)
            {
                _logger.LogInformation("Bid not found with ID {BidId}", bidId);
                return NotFound("Bid not found.");
            }

            return Ok(bid);
        }

        /// <summary>
        /// Get a winning bid for the current authenticated user.
        /// </summary>
        /// <returns>Bid details</returns>
        [HttpGet("winning")]
        [Authorize (Roles = "Seller")]
        [ProducesResponseType(typeof(BidResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserWinningBids()
        {
            var winningBids = await _bidService.GetUserWinningBidsAsync();
            if (winningBids == null)
            {
                _logger.LogInformation("Winnig bids was not found");
                return NotFound("Bid not found.");
            }
            return Ok(winningBids);
        }

        /// <summary>
        /// Get a history of bids placed by the current authenticated user.
        /// </summary>
        /// <returns>Bid details</returns>
        [HttpGet("history")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<BidResponseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserBidHistory()
        {
            var result = await _bidService.GetUserBidHistoryAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get a monthly statistics of bids placed by the current authenticated user.
        /// </summary>
        /// <returns>Bid details</returns>
        [HttpGet("monthly")]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(typeof(IEnumerable<MonthlyBidStatDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthlyStats()
        {
            var result = await _bidService.GetMonthlyUserBidStatsAsync();
            return Ok(result);
        }


        [HttpGet("admin/monthly")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<MonthlyBidStatDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSystemMonthlyStats()
        {
            var result = await _bidService.GetMonthlySystemBidStatsAsync();
            return Ok(result);
        }


        /// <summary>
        /// Donate a non-winning bid after auction ends.
        /// </summary>
        /// <param name="bidId">Bid identifier</param>
        /// <returns>Success status</returns>
        [HttpPost("donate/{bidId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DonateBid(Guid bidId)
        {
            try
            {
                var success = await _bidService.DonateBidAsync(bidId);

                if (!success)
                {
                    _logger.LogInformation("Failed to donate bid: {BidId}", bidId);
                    return BadRequest("Unable to donate this bid. It may have already been donated or is invalid.");
                }

                return Ok("Bid successfully donated.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized attempt to donate bid: {BidId}", bidId);
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Business rule violation while donating bid: {BidId}", bidId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while donating bid: {BidId}", bidId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
