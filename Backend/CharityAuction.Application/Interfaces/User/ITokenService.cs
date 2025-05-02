using CharityAuction.Domain.Entities;
using System.Threading.Tasks;

namespace CharityAuction.Application.Interfaces.User
{
    /// <summary>
    /// Service interface responsible for generating JWT access tokens and refresh tokens for users.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token should be generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated JWT token as a string.</returns>
        Task<string> GenerateTokenAsync(ApplicationUser user);

        /// <summary>
        /// Generates a secure refresh token.
        /// </summary>
        /// <returns>A newly generated refresh token as a string.</returns>
        string GenerateRefreshToken();
    }
}
