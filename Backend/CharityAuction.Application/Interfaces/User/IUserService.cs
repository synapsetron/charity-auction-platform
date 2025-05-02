using CharityAuction.Application.DTO.User;
using System;
using System.Threading.Tasks;

namespace CharityAuction.Application.Interfaces.User
{
    /// <summary>
    /// Service interface responsible for managing user-related operations such as registration, authentication, profile management, and token handling.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">The registration request data.</param>
        /// <returns>The created user's response DTO.</returns>
        Task<UserResponseDTO> RegisterAsync(UserRegisterDTO request);

        /// <summary>
        /// Authenticates a user with their credentials.
        /// </summary>
        /// <param name="request">The login request data.</param>
        /// <returns>The authenticated user's response DTO.</returns>
        Task<UserResponseDTO> LoginAsync(UserLoginDTO request);

        /// <summary>
        /// Confirms a user's email address.
        /// </summary>
        /// <param name="request">The email confirmation request data.</param>
        Task ConfirmEmailAsync(ConfirmEmailRequestDTO request);

        /// <summary>
        /// Initiates a password reset for a user.
        /// </summary>
        /// <param name="request">The password reset request data.</param>
        /// <returns>The result of the password reset operation.</returns>
        Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request);

        /// <summary>
        /// Retrieves the currently authenticated user's information.
        /// </summary>
        /// <returns>The current user's response DTO.</returns>
        Task<CurrentUserResponseDTO> GetCurrentUserAsync();

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user's unique ID.</param>
        /// <returns>The user's response DTO.</returns>
        Task<UserResponseDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Deletes a user by their unique identifier.
        /// </summary>
        /// <param name="id">The user's unique ID.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Revokes a user's refresh token to prevent further token refresh.
        /// </summary>
        /// <param name="refreshTokenRemoveRequest">The refresh token revoke request data.</param>
        /// <returns>The result of the refresh token revocation.</returns>
        Task<RevokeRefreshTokenResponseDTO> RevokeRefreshToken(RefreshTokenRequestDTO refreshTokenRemoveRequest);

        /// <summary>
        /// Refreshes the access token using a valid refresh token.
        /// </summary>
        /// <param name="request">The refresh token request data.</param>
        /// <returns>The new authentication information including new tokens.</returns>
        Task<CurrentUserResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request);

        /// <summary>
        /// Updates the profile information of the currently authenticated user.
        /// </summary>
        /// <param name="request">The profile update request data.</param>
        /// <returns>The updated user's response DTO.</returns>
        Task<UserResponseDTO> UpdateProfileAsync(UpdateUserRequestDTO request);
    }
}
