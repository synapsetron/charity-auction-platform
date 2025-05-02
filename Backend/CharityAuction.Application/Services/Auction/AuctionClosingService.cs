using CharityAuction.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CharityAuction.Application.Services
{
    /// <summary>
    /// Service responsible for automatically closing expired auctions and notifying winners.
    /// </summary>
    public class AuctionClosingService : IAuctionClosingService
    {
        private readonly IAuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly INotificationService _notificationService;
        private readonly INotificationSender _notificationSender;
        private readonly ILogger<AuctionClosingService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionClosingService"/> class.
        /// </summary>
        /// <param name="auctionService">Service for managing auctions.</param>
        /// <param name="bidService">Service for managing bids.</param>
        /// <param name="notificationService">Service for creating notifications.</param>
        /// <param name="notificationSender">Service for sending real-time notifications.</param>
        /// <param name="logger">Logger instance for diagnostics and audit.</param>
        /// <exception cref="ArgumentNullException">Thrown if any dependency is null.</exception>
        public AuctionClosingService(
            IAuctionService auctionService,
            IBidService bidService,
            INotificationService notificationService,
            INotificationSender notificationSender,
            ILogger<AuctionClosingService> logger)
        {
            _auctionService = auctionService ?? throw new ArgumentNullException(nameof(auctionService));
            _bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _notificationSender = notificationSender ?? throw new ArgumentNullException(nameof(notificationSender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task CloseExpiredAuctionsAsync()
        {
            _logger.LogInformation("Starting auction closure job at {StartTime}.", DateTime.UtcNow);
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var expiredAuctions = await _auctionService.GetExpiredAuctionsAsync();

                if (expiredAuctions == null || !expiredAuctions.Any())
                {
                    _logger.LogInformation("No expired auctions found to process.");
                    return;
                }

                foreach (var auction in expiredAuctions)
                {
                    try
                    {
                        _logger.LogInformation("Processing auction closure for AuctionId: {AuctionId}.", auction.Id);

                        var success = await _auctionService.ProcessAuctionEndAsync(auction.Id);

                        if (!success)
                        {
                            _logger.LogWarning("Auction {AuctionId} could not be closed (already closed or invalid state).", auction.Id);
                            continue;
                        }

                        await NotifyAllParticipantsAsync(auction.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while processing auction {AuctionId}.", auction.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Auction closure job failed unexpectedly.");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Auction closure job finished in {ElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Notifies all participants of the auction result.
        /// </summary>
        private async Task NotifyAllParticipantsAsync(Guid auctionId)
        {
            var bids = await _bidService.GetAllBidsForAuctionAsync(auctionId);

            if (!bids.Any())
            {
                _logger.LogInformation("No bids placed for AuctionId: {AuctionId}.", auctionId);
                return;
            }

            var highestBid = bids.OrderByDescending(b => b.Amount).First();
            var winnerId = highestBid.UserId;

            foreach (var bid in bids)
            {
                string title, message;

                if (bid.UserId == winnerId)
                {
                    title = "Вітаємо!";
                    message = $"Ви виграли аукціон '{bid.AuctionName}' з ставкою {bid.Amount:C}.";
                }
                else
                {
                    title = "Аукціон завершено";
                    message = $"Ви не виграли аукціон '{bid.AuctionName}', але дякуємо за участь!";
                }

                var notification = await _notificationService.CreateNotificationAsync(bid.UserId, title, message);
                await _notificationSender.SendNotificationAsync(bid.UserId, notification.Title, notification.Message);
            }

            _logger.LogInformation("Participants notified for auction {AuctionId}.", auctionId);
        }
    }
}
