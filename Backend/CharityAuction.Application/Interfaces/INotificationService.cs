using CharityAuction.Domain.Entities;

namespace CharityAuction.Application.Interfaces
{
    /// <summary>
    /// Defines operations for managing and creating notifications within the system.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Creates and stores a new notification for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user receiving the notification.</param>
        /// <param name="title">The title or subject of the notification.</param>
        /// <param name="message">The body or detailed message of the notification.</param>
        /// <returns>The created <see cref="Notification"/> entity.</returns>
        Task<Notification> CreateNotificationAsync(string userId, string title, string message);
    }
}
