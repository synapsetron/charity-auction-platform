using AutoMapper;
using CharityAuction.Application.DTO.Auction;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CharityAuction.Application.Services
{
    /// <summary>
    /// Service for creating, managing, retrieving, and closing auctions.
    /// </summary>
    public class AuctionService : IAuctionService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuctionService> _logger;
        private readonly IContentModerationService _moderationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        public AuctionService(
            IRepositoryWrapper repository,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<AuctionService> logger,
            IContentModerationService _moderationService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _moderationService = _moderationService ?? throw new ArgumentNullException(nameof(_moderationService));
        }

        #region Create & Update

        /// <inheritdoc/>
        public async Task<AuctionResponseDTO> CreateAuctionAsync(CreateAuctionRequestDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var organizerId = _currentUserService.GetUserId();
            if (string.IsNullOrWhiteSpace(organizerId))
            {
                _logger.LogWarning("Unauthorized attempt to create auction.");
                throw new UnauthorizedAccessException("You must be authenticated to create an auction.");
            }

            var auction = _mapper.Map<Auction>(request);
            auction.Id = Guid.NewGuid();
            auction.OrganizerId = organizerId;
            auction.CreatedAt = DateTime.UtcNow;
            auction.IsActive = false;
            auction.IsApproved = false;

            string combinedText = $"{auction.Title} {auction.Description}";
            var (isFlagged, reason) = await _moderationService.IsContentFlaggedAsync(combinedText);
            auction.IsFlagged = isFlagged;
            auction.FlaggedReason = reason;


            await _repository.AuctionRepository.CreateAsync(auction);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Auction {AuctionId} created successfully by User {UserId}.", auction.Id, organizerId);

            return _mapper.Map<AuctionResponseDTO>(auction);
        }

        /// <inheritdoc/>
        public async Task<AuctionResponseDTO> UpdateAuctionAsync(UpdateAuctionRequestDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id)
                          ?? throw new InvalidOperationException($"Auction with ID {request.Id} not found.");

            _mapper.Map(request, auction);

            _repository.AuctionRepository.Update(auction);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Auction {AuctionId} updated successfully.", auction.Id);

            return _mapper.Map<AuctionResponseDTO>(auction);
        }

        #endregion

        #region Retrieve Auctions

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionResponseWithBidsDTO>> GetAllAuctionsAsync()
        {
            _logger.LogInformation("Fetching all auctions.");
            return await FetchAuctionsAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionResponseWithBidsDTO>> GetActiveAuctionsAsync()
        {
            _logger.LogInformation("Fetching active auctions.");
            return await FetchAuctionsAsync(
                predicate: a => a.IsActive && a.EndTime > DateTime.UtcNow
            );
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionResponseWithBidsDTO>> GetExpiredAuctionsAsync()
        {
            _logger.LogInformation("Fetching expired auctions.");
            return await FetchAuctionsAsync(
                predicate: a => a.IsActive && a.EndTime <= DateTime.UtcNow
            );
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionResponseWithBidsDTO>> GetApprovedAuctionsAsync()
        {
            _logger.LogInformation("Fetching approved auctions.");
            return await FetchAuctionsAsync(
                predicate: a => a.IsApproved && a.IsActive && a.EndTime > DateTime.UtcNow
            );
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuctionResponseWithBidsDTO>> GetMyAuctionsAsync()
        {
            _logger.LogInformation("Fetching auctions created by the current user.");

            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogWarning("Unauthorized attempt to retrieve personal auctions.");
                throw new UnauthorizedAccessException("User must be authenticated to retrieve personal auctions.");
            }

            return await FetchAuctionsAsync(
                predicate: a => a.OrganizerId == userId
            );
        }
        /// <inheritdoc/>
        public async Task<AuctionResponseWithBidsDTO?> GetAuctionByIdAsync(Guid auctionId)
        {
            if (auctionId == Guid.Empty)
            {
                _logger.LogWarning("Invalid auction ID provided for retrieval.");
                throw new ArgumentException("Auction ID cannot be empty.", nameof(auctionId));
            }

            try
            {
                _logger.LogInformation("Fetching auction details for ID: {AuctionId}", auctionId);

                var auctions = await FetchAuctionsAsync(
                    predicate: a => a.Id == auctionId,
                    include: q => q.Include(a => a.Bids).ThenInclude(b => b.User)
                );

                var auction = auctions.FirstOrDefault();
                if (auction == null)
                {
                    _logger.LogWarning("Auction with ID {AuctionId} not found.", auctionId);
                    return null;
                }

                _logger.LogInformation("Auction {AuctionId} successfully fetched and mapped.", auctionId);
                return auction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching auction {AuctionId}.", auctionId);
                throw;
            }
        }


        #endregion

        #region Sales & Donations

        /// <inheritdoc/>
        public async Task<decimal> GetTotalDonatedAmountAsync()
        {
            var soldAuctionSum = await _repository.AuctionRepository
                .FindAll(a => a.IsSold && a.FinalPrice.HasValue)
                .SumAsync(a => a.FinalPrice ?? 0);

            var donatedBidsSum = await _repository.BidRepository
                .FindAll(b => b.IsDonated)
                .SumAsync(b => b.Amount);

            var total = soldAuctionSum + donatedBidsSum;
            _logger.LogInformation("Total donated amount calculated: {Total}", total);

            return total;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RecentSaleDTO>> GetRecentSalesAsync()
        {
            var auctions = await _repository.AuctionRepository
                .FindAll(a => a.IsSold && a.FinalPrice.HasValue)
                .OrderByDescending(a => a.EndTime)
                .Take(10)
                .ToListAsync();

            var result = auctions.Select(a => new RecentSaleDTO
            {
                Title = a.Title,
                SoldPrice = a.FinalPrice ?? 0,
                SoldAt = a.EndTime,
                FundName = "Armed Forces Support Fund"
            });

            _logger.LogInformation("Fetched {Count} recent sales.", result.Count());

            return result;
        }

        #endregion

        #region Auction Closing

        /// <inheritdoc/>
        public async Task<bool> ProcessAuctionEndAsync(Guid auctionId)
        {
            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(
                a => a.Id == auctionId,
                include: q => q.Include(a => a.Bids)
            );

            if (!auction.IsActive)
            {
                _logger.LogWarning("Auction {AuctionId} already inactive.", auctionId);
                return false;
            }

            if (auction.EndTime > DateTime.UtcNow)
            {
                _logger.LogWarning("Auction {AuctionId} not yet ended. EndTime: {EndTime}, Now: {Now}", auction.Id, auction.EndTime, DateTime.UtcNow);
                return false;
            }


            var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

            if (highestBid != null)
            {
                auction.FinalPrice = highestBid.Amount;
                auction.IsSold = false;
            }

            auction.IsActive = false;

            _repository.AuctionRepository.Update(auction);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Auction {AuctionId} closed and winner processed successfully.", auctionId);

            return true;
        }

        #endregion


        /// <summary>
        /// Private helper to safely fetch and map auctions with common error handling and logging.
        /// </summary>
        /// <param name="predicate">Optional predicate to filter auctions.</param>
        /// <param name="include">Optional includes for related entities.</param>
        /// <returns>A safe collection of <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        /// <summary>
        /// Private helper to safely fetch and map auctions with common error handling and logging.
        /// </summary>
        /// <param name="predicate">Optional predicate to filter auctions.</param>
        /// <param name="include">Optional includes for related entities.</param>
        /// <returns>A safe collection of <see cref="AuctionResponseWithBidsDTO"/>.</returns>
        private async Task<IEnumerable<AuctionResponseWithBidsDTO>> FetchAuctionsAsync(
            Expression<Func<Auction, bool>>? predicate = null,
            Func<IQueryable<Auction>, IIncludableQueryable<Auction, object>>? include = null)
        {
            try
            {
                var auctions = await _repository.AuctionRepository.GetAllAsync(
                    predicate,
                    include ?? (q => q.Include(a => a.Bids).ThenInclude(b => b.User))
                );

                if (auctions == null)
                {
                    _logger.LogWarning("Auction repository returned null.");
                    return Enumerable.Empty<AuctionResponseWithBidsDTO>();
                }

                var mappedAuctions = _mapper.Map<IEnumerable<AuctionResponseWithBidsDTO>>(auctions);

                if (mappedAuctions == null)
                {
                    _logger.LogError("Failed to map auctions to DTOs.");
                    return Enumerable.Empty<AuctionResponseWithBidsDTO>();
                }

                _logger.LogInformation("{Count} auctions successfully fetched and mapped.", mappedAuctions.Count());
                return mappedAuctions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching auctions.");
                return Enumerable.Empty<AuctionResponseWithBidsDTO>();
            }
        }

    }
}
