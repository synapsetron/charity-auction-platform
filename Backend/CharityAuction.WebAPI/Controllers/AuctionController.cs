using CharityAuction.Application.DTO.Auction;
using CharityAuction.Application.Interfaces;
using CharityAuction.WebAPI.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CharityAuction.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing auctions.
    /// </summary>
    [ApiController]
    [Route("api/v1/auctions")]
    [Authorize]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly ILogger<AuctionController> _logger;
        private readonly IHubContext<AuctionHub> _auctionHub;
        private readonly INotificationService _notificationService;

        public AuctionController(
            IAuctionService auctionService,
            ILogger<AuctionController> logger,
            IHubContext<AuctionHub> auctionHub,
            INotificationService notificationService,
            IBidService bidService)
        {
            _auctionService = auctionService;
            _logger = logger;
            _auctionHub = auctionHub;
            _notificationService = notificationService;
            _bidService = bidService;
        }

        /// <summary>
        /// Creates a new auction.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AuctionResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionRequestDTO request)
        {
            if (request == null)
            {
                _logger.LogWarning("CreateAuction request body is null.");
                return BadRequest(new { Message = "Request body cannot be null." });
            }

            try
            {
                var auction = await _auctionService.CreateAuctionAsync(request);
                return CreatedAtAction(nameof(GetAuctionById), new { auctionId = auction.Id }, auction);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access during auction creation.");
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during auction creation.");
                return StatusCode(500, new { Message = "An unexpected error occurred while creating the auction." });
            }
        }

        /// <summary>
        /// Retrieves an auction by its ID.
        /// </summary>
        [HttpGet("{auctionId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuctionResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuctionById(Guid auctionId)
        {
            try
            {
                var auction = await _auctionService.GetAuctionByIdAsync(auctionId);

                if (auction == null)
                {
                    _logger.LogWarning("Auction with ID {AuctionId} not found.", auctionId);
                    return NotFound(new { Message = "Auction not found." });
                }

                return Ok(auction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving auction {AuctionId}.", auctionId);
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving the auction." });
            }
        }

        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        [HttpGet("all")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AuctionResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAuctions()
        {
            try
            {
                var auctions = await _auctionService.GetAllAuctionsAsync();
                return Ok(auctions ?? Enumerable.Empty<AuctionResponseDTO>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all auctions.");
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving auctions." });
            }
        }

        /// <summary>
        /// Retrieves all active auctions.
        /// </summary>
        [HttpGet("active")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AuctionResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetActiveAuctions()
        {
            try
            {
                var auctions = await _auctionService.GetActiveAuctionsAsync();
                return Ok(auctions ?? Enumerable.Empty<AuctionResponseDTO>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve active auctions.");
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving active auctions." });
            }
        }

        /// <summary>
        /// Updates an existing auction.
        /// </summary>
        [HttpPut("update")]
        [Authorize(Roles = "Admin,Seller")]
        [ProducesResponseType(typeof(AuctionResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAuction([FromBody] UpdateAuctionRequestDTO request)
        {
            if (request == null)
            {
                _logger.LogWarning("UpdateAuction request body is null.");
                return BadRequest(new { Message = "Request body cannot be null." });
            }

            try
            {
                var auction = await _auctionService.UpdateAuctionAsync(request);
                return Ok(auction);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation during auction update.");
                return BadRequest(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access during auction update.");
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating auction.");
                return StatusCode(500, new { Message = "An unexpected error occurred while updating the auction." });
            }
        }

        /// <summary>
        /// Retrieves the total amount of donations.
        /// </summary>
        [HttpGet("total-donations")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalDonatedAmount()
        {
            try
            {
                var totalAmount = await _auctionService.GetTotalDonatedAmountAsync();
                return Ok(totalAmount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve total donated amount.");
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving the total amount." });
            }
        }

        /// <summary>
        /// Retrieves the list of recent auction sales.
        /// </summary>
        [HttpGet("recent-sales")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<RecentSaleDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecentSales()
        {
            try
            {
                var sales = await _auctionService.GetRecentSalesAsync();
                return Ok(sales ?? Enumerable.Empty<RecentSaleDTO>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve recent sales.");
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving recent sales." });
            }
        }

        /// <summary>
        /// Retrieves all approved auctions.
        /// </summary>
        [HttpGet("approved")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AuctionResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApprovedAuctions()
        {
            try
            {
                var auctions = await _auctionService.GetApprovedAuctionsAsync();
                return Ok(auctions ?? Enumerable.Empty<AuctionResponseDTO>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve approved auctions.");
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving approved auctions." });
            }
        }

        /// <summary>
        /// Retrieves auctions created by the current authenticated user.
        /// </summary>
        [HttpGet("my-auctions")]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(typeof(IEnumerable<AuctionResponseWithBidsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyAuctions()
        {
            try
            {
                var auctions = await _auctionService.GetMyAuctionsAsync();
                return Ok(auctions ?? Enumerable.Empty<AuctionResponseWithBidsDTO>());
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access to my auctions.");
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving my auctions.");
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving your auctions." });
            }
        }
    }
}
