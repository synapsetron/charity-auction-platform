using CharityAuction.Application.DTO.User;
using CharityAuction.Application.DTO.User.GoogleUser;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Infrastructure.Options;
using CharityAuction.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CharityAuction.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication and authorization.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IOptions<JwtSettingsOptions> _jwtOptions;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUserService userService,
            IGoogleAuthService googleAuthService,
            IOptions<JwtSettingsOptions> jwtOptions,
            ILogger<AuthController> logger)
        {
            _userService = userService;
            _googleAuthService = googleAuthService;
            _jwtOptions = jwtOptions;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The user registration request.</param>
        /// <returns>The created user's information.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Validation failed.</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO request)
        {
            try
            {
                var response = await _userService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed.");
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Authenticates a user and sets the access token.
        /// </summary>
        /// <param name="request">The login credentials.</param>
        /// <returns>User information with tokens.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Invalid credentials.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO request)
        {
            try
            {
                var response = await _userService.LoginAsync(request);
                HttpContext.AppendTokenToCookie(response.AccessToken, _jwtOptions);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed.");
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Authenticates a user via Google OAuth.
        /// </summary>
        /// <param name="request">Google login request.</param>
        /// <returns>User information with tokens.</returns>
        /// <response code="200">Google login successful.</response>
        /// <response code="400">Invalid Google token.</response>
        [HttpPost("login-google")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.IdToken))
                return BadRequest(new { Message = "IdToken is required." });

            try
            {
                var response = await _googleAuthService.LoginWithGoogleAsync(request.IdToken);
                HttpContext.AppendTokenToCookie(response.AccessToken, _jwtOptions);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Google login failed.");
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Logs out the user by clearing the authentication cookie.
        /// </summary>
        /// <returns>Logout success message.</returns>
        /// <response code="200">User logged out successfully.</response>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {
            HttpContext.DeleteAuthTokenCookie();
            return Ok(new { Message = "Logout successful." });
        }

        /// <summary>
        /// Refreshes the user's access token.
        /// </summary>
        /// <param name="request">Refresh token request.</param>
        /// <returns>New access token and user info.</returns>
        /// <response code="200">Token refreshed successfully.</response>
        /// <response code="400">Invalid refresh token.</response>
        [HttpPost("refresh-token")]
        [Authorize]
        [ProducesResponseType(typeof(CurrentUserResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            try
            {
                var response = await _userService.RefreshTokenAsync(request);
                HttpContext.AppendTokenToCookie(response.AccessToken, _jwtOptions);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token refresh failed.");
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Revokes the user's refresh token.
        /// </summary>
        /// <param name="request">Refresh token to revoke.</param>
        /// <returns>Revoke operation status.</returns>
        /// <response code="200">Refresh token revoked successfully.</response>
        /// <response code="400">Failed to revoke refresh token.</response>
        [HttpPost("revoke-refresh-token")]
        [Authorize]
        [ProducesResponseType(typeof(RevokeRefreshTokenResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            try
            {
                var response = await _userService.RevokeRefreshToken(request);

                if (response?.Message == "Refresh token revoked successfully")
                {
                    HttpContext.DeleteAuthTokenCookie();
                    return Ok(response);
                }

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to revoke refresh token.");
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>User data.</returns>
        /// <response code="200">User found.</response>
        /// <response code="404">User not found.</response>
        [HttpGet("user/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _userService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user by ID.");
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the currently authenticated user's profile.
        /// </summary>
        /// <returns>Current user data.</returns>
        /// <response code="200">User data retrieved successfully.</response>
        [HttpGet("current-user")]
        [Authorize]
        [ProducesResponseType(typeof(CurrentUserResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var response = await _userService.GetCurrentUserAsync();
            return Ok(response);
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="request">Reset password request.</param>
        /// <returns>New password details.</returns>
        /// <response code="200">Password reset successfully.</response>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResetPasswordResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            var response = await _userService.ResetPasswordAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Confirms a user's email address.
        /// </summary>
        /// <param name="request">Email confirmation request.</param>
        /// <returns>Confirmation status.</returns>
        /// <response code="200">Email confirmed successfully.</response>
        /// <response code="400">Invalid confirmation token or user not found.</response>
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestDTO request)
        {
            try
            {
                await _userService.ConfirmEmailAsync(request);
                return Ok(new { Message = "Email confirmed successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Email confirmation failed.");
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the authenticated user's profile.
        /// </summary>
        /// <param name="request">Profile update data.</param>
        /// <returns>Updated user data.</returns>
        /// <response code="200">Profile updated successfully.</response>
        [HttpPut("user/update-profile")]
        [Authorize]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserRequestDTO request)
        {
            var response = await _userService.UpdateProfileAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Status message.</returns>
        /// <response code="200">User deleted successfully.</response>
        /// <response code="404">User not found.</response>
        [HttpDelete("user/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok(new { Message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete user.");
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}