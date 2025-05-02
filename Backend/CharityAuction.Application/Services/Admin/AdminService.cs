using CharityAuction.Application.DTO.Auction;
using CharityAuction.Application.DTO.User;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.Admin;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CharityAuction.Application.Services.Admin
{
    /// <summary>
    /// Provides administrative operations for managing auctions and users.
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger<AdminService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationSender _notificationSender;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminService"/> class.
        /// </summary>
        public AdminService(
            IRepositoryWrapper repository,
            ILogger<AdminService> logger,
            UserManager<ApplicationUser> userManager,
            INotificationSender notificationSender,
            INotificationService notificationService,
            IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _notificationSender = notificationSender ?? throw new ArgumentNullException(nameof(notificationSender));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Auctions

        public async Task<IEnumerable<AuctionResponseDTO>> GetPendingAuctionsAsync()
        {
            _logger.LogInformation("Fetching pending auctions.");

            var auctions = await _repository.AuctionRepository.GetAllAsync(a => !a.IsApproved);

            if (auctions == null)
            {
                _logger.LogWarning("Auction repository returned null when fetching pending auctions.");
                return Enumerable.Empty<AuctionResponseDTO>();
            }

            if (!auctions.Any())
            {
                _logger.LogInformation("No pending auctions found.");
                return Enumerable.Empty<AuctionResponseDTO>();
            }

            var auctionDtos = _mapper.Map<IEnumerable<AuctionResponseDTO>>(auctions);

            if (auctionDtos == null)
            {
                _logger.LogError("Mapping auctions to DTOs failed.");
                throw new ApplicationException("Failed to map pending auctions.");
            }

            _logger.LogInformation("Successfully fetched {Count} pending auctions.", auctionDtos.Count());

            return auctionDtos;
        }

        public async Task<bool> ApproveAuctionAsync(Guid auctionId)
        {
            if (auctionId == Guid.Empty) throw new ArgumentException("Auction ID cannot be empty.", nameof(auctionId));

            _logger.LogInformation("Approving auction {AuctionId}.", auctionId);

            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null)
            {
                _logger.LogWarning("Auction {AuctionId} not found.", auctionId);
                return false;
            }

            auction.IsApproved = true;
            auction.IsActive = true;
            _repository.AuctionRepository.Update(auction);

            await NotifyUserAsync(auction.OrganizerId, "Auction Approved", $"Your auction '{auction.Title}' has been approved.");

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAuctionAsync(Guid auctionId, string rejectionReason)
        {
            if (auctionId == Guid.Empty) throw new ArgumentException("Auction ID cannot be empty.", nameof(auctionId));
            if (string.IsNullOrWhiteSpace(rejectionReason)) throw new ArgumentException("Rejection reason must be provided.", nameof(rejectionReason));

            _logger.LogInformation("Rejecting auction {AuctionId}.", auctionId);

            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null)
            {
                _logger.LogWarning("Auction {AuctionId} not found.", auctionId);
                return false;
            }

            _repository.AuctionRepository.Delete(auction);

            await NotifyUserAsync(auction.OrganizerId, "Auction Rejected", $"Your auction '{auction.Title}' was rejected: {rejectionReason}");

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BlockAuctionAsync(Guid auctionId)
        {
            _logger.LogInformation("Blocking auction {AuctionId}.", auctionId);
            return await ChangeAuctionStateAsync(auctionId, false, "Auction Blocked", "Your auction '{0}' has been blocked.");
        }

        public async Task<bool> UnblockAuctionAsync(Guid auctionId)
        {
            _logger.LogInformation("Unblocking auction {AuctionId}.", auctionId);
            return await ChangeAuctionStateAsync(auctionId, true, "Auction Unblocked", "Your auction '{0}' has been unblocked.");
        }

        public async Task<bool> DeleteAuctionAsync(Guid auctionId)
        {
            _logger.LogInformation("Deleting auction {AuctionId}.", auctionId);

            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null)
            {
                _logger.LogWarning("Auction {AuctionId} not found.", auctionId);
                return false;
            }

            _repository.AuctionRepository.Delete(auction);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Auction {AuctionId} deleted successfully.", auctionId);
            return true;
        }

        public async Task<bool> CloseAuctionAsync(Guid auctionId)
        {
            _logger.LogInformation("Closing auction {AuctionId}.", auctionId);
            return await ChangeAuctionStateAsync(auctionId, false, null, null);
        }

        private async Task<bool> ChangeAuctionStateAsync(Guid auctionId, bool isActive, string? title, string? messageTemplate)
        {
            if (auctionId == Guid.Empty) throw new ArgumentException("Auction ID cannot be empty.", nameof(auctionId));

            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null)
            {
                _logger.LogWarning("Auction {AuctionId} not found.", auctionId);
                return false;
            }

            auction.IsActive = isActive;
            _repository.AuctionRepository.Update(auction);

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(messageTemplate))
            {
                await NotifyUserAsync(auction.OrganizerId, title, string.Format(messageTemplate, auction.Title));
            }

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAuctionAsPaidAsync(Guid auctionId, decimal? paidAmount)
        {
            var auction = await _repository.AuctionRepository.GetFirstOrDefaultAsync(a => a.Id == auctionId);
            if (auction == null) return false;

            auction.IsSold = true;
            auction.FinalPrice = paidAmount ?? auction.FinalPrice;

            _repository.AuctionRepository.Update(auction);
            await _repository.SaveChangesAsync();

            return true;
        }

        #endregion
        #region Users

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {
            _logger.LogInformation("Fetching all users.");
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserResponseDTO>>(users);
        }

        public async Task<bool> BlockUserAsync(string userId)
        {
            return await ChangeUserLockoutStateAsync(userId, true);
        }

        public async Task<bool> UnblockUserAsync(string userId)
        {
            return await ChangeUserLockoutStateAsync(userId, false);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            _logger.LogInformation("Deleting user {UserId}.", userId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found.", userId);
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Failed to delete user {UserId}.", userId);
                return false;
            }

            _logger.LogInformation("User {UserId} deleted successfully.", userId);
            return true;
        }

        private async Task<bool> ChangeUserLockoutStateAsync(string userId, bool isLocked)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found.", userId);
                return false;
            }

            user.LockoutEnabled = isLocked;
            user.LockoutEnd = isLocked ? DateTimeOffset.UtcNow.AddYears(100) : null;

            await _userManager.UpdateAsync(user);

            string action = isLocked ? "blocked" : "unblocked";
            _logger.LogInformation("User {UserId} {Action} successfully.", userId, action);

            return true;
        }

        #endregion

        private async Task NotifyUserAsync(string userId, string title, string message)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.NotificationRepository.CreateAsync(notification);
            await _notificationSender.SendNotificationAsync(userId, title, message); // <--- 💥 null тут
        }


    }
}
