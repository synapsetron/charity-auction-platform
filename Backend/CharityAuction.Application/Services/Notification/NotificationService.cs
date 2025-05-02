using Microsoft.Extensions.Logging;
using CharityAuction.Application.Interfaces;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;

namespace CharityAuction.Application.Services
{
    /// <summary>
    /// Service responsible for creating and managing notifications for users.
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger<NotificationService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="repository">The repository wrapper to access notification data store.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public NotificationService(IRepositoryWrapper repository, ILogger<NotificationService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<Notification> CreateNotificationAsync(string userId, string title, string message)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Notification title cannot be null or empty.", nameof(title));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Notification message cannot be null or empty.", nameof(message));

            try
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = title,
                    Message = message,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                _logger.LogInformation("Creating notification for UserId: {UserId}, Title: {Title}", userId, title);

                await _repository.NotificationRepository.CreateAsync(notification);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Notification created successfully. NotificationId: {NotificationId}", notification.Id);

                return notification;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create notification for UserId: {UserId}", userId);
                throw new InvalidOperationException("Failed to create notification.", ex);
            }
        }
    }
}
