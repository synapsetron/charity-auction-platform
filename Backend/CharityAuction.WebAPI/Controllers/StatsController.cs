using CharityAuction.Application.DTO.Statistics;
using CharityAuction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CharityAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;
        private readonly ILogger<StatsController> _logger;

        public StatsController(IStatsService statsService, ILogger<StatsController> logger)
        {
            _statsService = statsService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves statistical overview for the current authenticated user.
        /// </summary>
        /// <remarks>
        /// The returned data varies based on the user's role:
        /// - Sellers: winning bids, number of auctions, balance
        /// - Admins: total users, auctions, monthly bid chart
        /// - Buyers: total bids, donated bids
        /// </remarks>
        /// <returns>A role-specific statistical overview.</returns>
        /// <response code="200">Returns the statistics for the user</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="500">An internal server error occurred</response>
        [HttpGet("overview")]
        [Authorize]
        [ProducesResponseType(typeof(StatsOverviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOverview()
        {
            try
            {
                _logger.LogInformation("Fetching statistics overview for user.");
                var stats = await _statsService.GetOverviewAsync();
                return Ok(stats);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Unauthorized access while fetching stats: {Message}", ex.Message);
                return Unauthorized("User is not authorized.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching statistics overview.");
                return StatusCode(500, "An error occurred while retrieving statistics.");
            }
        }
    }
}
