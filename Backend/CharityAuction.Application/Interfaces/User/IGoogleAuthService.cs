using CharityAuction.Application.DTO.User;

namespace CharityAuction.Application.Interfaces.User
{
    /// <summary>
    /// Service interface for handling user authentication via Google OAuth.
    /// </summary>
    public interface IGoogleAuthService
    {
        /// <summary>
        /// Authenticates a user using a Google ID token and returns the user's information.
        /// </summary>
        /// <param name="idToken">The ID token received from Google's authentication provider.</param>
        /// <returns>
        /// A <see cref="UserResponseDTO"/> containing information about the authenticated user.
        /// </returns>
        public Task<UserResponseDTO> LoginWithGoogleAsync(string idToken);
    }
}
