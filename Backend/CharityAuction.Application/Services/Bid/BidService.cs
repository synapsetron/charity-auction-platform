using AutoMapper;
using CharityAuction.Application.DTO.Bid;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CharityAuction.Application.Services
{
    /// <summary>
    /// Service for handling bid-related operations including placement, retrieval, and donation.
    /// </summary>
    public class BidService : IBidService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<BidService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public BidService(
            IRepositoryWrapper repository,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<BidService> logger,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <inheritdoc/>
        public async Task<BidResponseDTO> PlaceBidAsync(CreateBidRequestDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Amount <= 0) throw new ArgumentException("Bid amount must be positive.", nameof(request.Amount));

            _logger.LogInformation("Placing bid for AuctionId: {AuctionId} with Amount: {Amount}", request.AuctionId, request.Amount);

            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == request.AuctionId);
            if (auction == null || !auction.IsActive)
            {
                _logger.LogWarning("Auction not found or inactive: {AuctionId}", request.AuctionId);
                throw new InvalidOperationException("Auction not found or inactive.");
            }

            var highestBid = _repository.BidRepository
                .FindAll(b => b.AuctionId == request.AuctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefault();

            var minimumAmount = highestBid != null ? highestBid.Amount + 1 : auction.StartingPrice;
            if (request.Amount < minimumAmount)
            {
                _logger.LogWarning("Bid too low for AuctionId: {AuctionId}. Minimum: {MinimumAmount}, Provided: {ProvidedAmount}",
                    request.AuctionId, minimumAmount, request.Amount);
                throw new InvalidOperationException($"Bid must be at least {minimumAmount}.");
            }

            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Unauthorized user attempted to place bid.");
                throw new UnauthorizedAccessException("User must be authenticated to place a bid.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError("User not found: {UserId}", userId);
                throw new KeyNotFoundException("User not found.");
            }

            var bid = _mapper.Map<Bid>(request);
            bid.Id = Guid.NewGuid();
            bid.User = user;
            bid.CreatedAt = DateTime.UtcNow;

            await _repository.BidRepository.CreateAsync(bid);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Bid successfully placed. BidId: {BidId}", bid.Id);

            return _mapper.Map<BidResponseDTO>(bid);
        }

        /// <inheritdoc/>
        public async Task<bool> DonateBidAsync(Guid bidId)
        {
            if (bidId == Guid.Empty) throw new ArgumentException("Bid ID cannot be empty.", nameof(bidId));

            _logger.LogInformation("Processing donation for BidId: {BidId}", bidId);

            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User must be authenticated.");

            var bid = await _repository.BidRepository.GetFirstOrDefaultAsync(b => b.Id == bidId, include: q => q.Include(b => b.Auction));
            if (bid == null)
            {
                _logger.LogWarning("Bid not found for donation: {BidId}", bidId);
                return false;
            }

            if (bid.UserId != userId)
            {
                _logger.LogWarning("User {UserId} attempted to donate unauthorized bid {BidId}.", userId, bidId);
                throw new UnauthorizedAccessException("Cannot donate someone else's bid.");
            }

            if (bid.IsDonated)
            {
                _logger.LogInformation("Bid already donated: {BidId}", bidId);
                return false;
            }

            if (bid.Auction.IsActive || bid.Auction.EndTime > DateTime.UtcNow)
            {
                _logger.LogWarning("Attempt to donate for active auction: {AuctionId}", bid.AuctionId);
                throw new InvalidOperationException("Cannot donate for an active auction.");
            }

            var highestBid = await _repository.BidRepository
                .FindAll(b => b.AuctionId == bid.AuctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            if (highestBid != null && highestBid.Id == bid.Id)
            {
                _logger.LogWarning("Winner attempted to donate winning bid: {BidId}", bidId);
                throw new InvalidOperationException("Cannot donate a winning bid.");
            }

            bid.IsDonated = true;
            _repository.BidRepository.Update(bid);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Bid successfully donated: {BidId}", bidId);
            return true;
        }




#region Get Bids
        /// <inheritdoc/>
        public async Task<BidResponseDTO?> GetHighestBidAsync(Guid auctionId)
        {
            if (auctionId == Guid.Empty) throw new ArgumentException("Auction ID cannot be empty.", nameof(auctionId));

            _logger.LogInformation("Fetching highest bid for AuctionId: {AuctionId}", auctionId);

            var bid = await _repository.BidRepository
                .FindAll(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();

            return bid != null ? _mapper.Map<BidResponseDTO>(bid) : null;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BidResponseDTO>> GetAllBidsForAuctionAsync(Guid auctionId)
        {
            if (auctionId == Guid.Empty) throw new ArgumentException("Auction ID cannot be empty.", nameof(auctionId));

            var bids = await _repository.BidRepository.GetAllAsync(b => b.AuctionId == auctionId);
            var sortedBids = bids.OrderByDescending(b => b.CreatedAt);

            return _mapper.Map<IEnumerable<BidResponseDTO>>(sortedBids) ?? Enumerable.Empty<BidResponseDTO>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BidResponseDTO>> GetUserBidsByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            var bids = await _repository.BidRepository.GetAllAsync(b => b.UserId == userId);
            var sortedBids = bids.OrderByDescending(b => b.CreatedAt);

            return _mapper.Map<IEnumerable<BidResponseDTO>>(sortedBids) ?? Enumerable.Empty<BidResponseDTO>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BidResponseDTO>> GetUserBidsAsync()
        {
            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User must be authenticated.");

            var bids = await _repository.BidRepository.GetAllAsync(b => b.UserId == userId);
            var sortedBids = bids.OrderByDescending(b => b.CreatedAt);

            return _mapper.Map<IEnumerable<BidResponseDTO>>(sortedBids) ?? Enumerable.Empty<BidResponseDTO>();
        }

        /// <inheritdoc/>
        public async Task<BidResponseDTO?> GetBidByIdAsync(Guid bidId)
        {
            if (bidId == Guid.Empty) throw new ArgumentException("Bid ID cannot be empty.", nameof(bidId));

            var bid = await _repository.BidRepository.GetFirstOrDefaultAsync(b => b.Id == bidId);
            return bid != null ? _mapper.Map<BidResponseDTO>(bid) : null;
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<BidResponseDTO>> GetUserWinningBidsAsync()
        {
            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User must be authenticated.");

            var endedAuctions = await _repository.AuctionRepository
                .FindAll(a => !a.IsActive && a.EndTime <= DateTime.UtcNow)
                .ToListAsync();

            var auctionIds = endedAuctions.Select(a => a.Id).ToList();

            var userBids = await _repository.BidRepository
                .FindAll(b => auctionIds.Contains(b.AuctionId) && b.UserId == userId)
                .ToListAsync();

            var winningBids = new List<Bid>();

            foreach (var auctionId in auctionIds)
            {
                var bidsForAuction = userBids.Where(b => b.AuctionId == auctionId);
                var topBid = bidsForAuction.OrderByDescending(b => b.Amount).FirstOrDefault();
                if (topBid != null)
                {
                    winningBids.Add(topBid);
                }
            }

            return _mapper.Map<IEnumerable<BidResponseDTO>>(winningBids);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<BidResponseWithWinnerDTO>> GetUserBidHistoryAsync()
        {
            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User must be authenticated.");

            var bids = await _repository.BidRepository
                .FindAll(b => b.UserId == userId)
                .Include(b => b.Auction)
                .ToListAsync();

            var groupedByAuction = bids.GroupBy(b => b.AuctionId).ToList();

            var allAuctionIds = groupedByAuction.Select(g => g.Key).ToList();

            // отримуємо найвищі ставки по кожному аукціону
            var highestBids = await _repository.BidRepository
                .FindAll(b => allAuctionIds.Contains(b.AuctionId))
                .GroupBy(b => b.AuctionId)
                .Select(g => g.OrderByDescending(b => b.Amount).First())
                .ToListAsync();

            var highestBidDict = highestBids.ToDictionary(b => b.AuctionId, b => b.Id);

            var responseList = new List<BidResponseWithWinnerDTO>();

            foreach (var bid in bids)
            {
                var dto = _mapper.Map<BidResponseWithWinnerDTO>(bid);
                dto.isWinner = highestBidDict.TryGetValue(bid.AuctionId, out var winnerId) && winnerId == bid.Id;
                responseList.Add(dto);
            }

            return responseList.OrderByDescending(b => b.CreatedAt);
        }
        /// <inheritdoc/>
        public async Task<IEnumerable<Bid>> GetAllBidsWithDetailsAsync()
        {
            return await _repository.BidRepository
                .FindAll()
                .Include(b => b.User)
                .Include(b => b.Auction)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MonthlyBidStatDTO>> GetMonthlyUserBidStatsAsync()
        {
            var userId = _currentUserService.GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User must be authenticated.");

            var bids = await _repository.BidRepository
                .FindAll(b => b.UserId == userId)
                .ToListAsync();

            var stats = bids
                .GroupBy(b => new { b.CreatedAt.Year, b.CreatedAt.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new MonthlyBidStatDTO
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("uk-UA")),
                    Count = g.Count()
                });

            return stats;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<MonthlyBidStatDTO>> GetMonthlySystemBidStatsAsync()
        {
            var bids = await _repository.BidRepository
                .FindAll()
                .ToListAsync();

            var stats = bids
                .GroupBy(b => new { b.CreatedAt.Year, b.CreatedAt.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new MonthlyBidStatDTO
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1)
                        .ToString("MMMM yyyy", new System.Globalization.CultureInfo("uk-UA")),
                    Count = g.Count()
                });

            return stats;
        }

        #endregion
    }
}
