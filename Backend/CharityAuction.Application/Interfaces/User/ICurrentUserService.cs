namespace CharityAuction.Application.Interfaces.User
{
    /// <summary>
    /// Service interface for retrieving information about the currently authenticated user.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the unique identifier of the currently authenticated user.
        /// </summary>
        /// <returns>
        /// A string representing the user's ID if authenticated; otherwise, <c>null</c>.
        /// </returns>
        string? GetUserId();
    }
}
