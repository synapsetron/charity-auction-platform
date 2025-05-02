namespace CharityAuction.Application.Interfaces
{
    /// <summary>
    /// Defines functionality for sending real-time or system notifications to users.
    /// </summary>
    public interface INotificationSender
    {
        /// <summary>
        /// Sends a notification message to a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the target user.</param>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The body or description of the notification.</param>
        /// <returns>A task that represents the asynchronous send operation.</returns>
        Task SendNotificationAsync(string userId, string title, string message);
    }
}
