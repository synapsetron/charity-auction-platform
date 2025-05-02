using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CharityAuction.Application.DTO.User;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Domain.Entities;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.IdentityModel.Tokens.Jwt;

namespace CharityAuction.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailSender _emailSender;
        public UserService(ITokenService tokenService, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager,
            IMapper mapper, ILogger<UserService> logger,IEmailSender emailSender)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        #region Login/Register
        public async Task<UserResponseDTO> RegisterAsync(UserRegisterDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Attempting to register user {Email}", request.Email);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email already exists.");

            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.UserName = await GenerateUniqueUserNameAsync(request.FirstName, request.LastName);
            newUser.CreatedAt = DateTime.UtcNow;
            newUser.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _tokenService.GenerateTokenAsync(newUser);
            _logger.LogInformation("User {UserId} registered successfully", newUser.Id);

            return _mapper.Map<UserResponseDTO>(newUser);
        }
        public async Task<UserResponseDTO> LoginAsync(UserLoginDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _logger.LogInformation("Login attempt for {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Invalid email or password.");

            var accessToken = await _tokenService.GenerateTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = HashRefreshToken(refreshToken);
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(2);
            user.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new InvalidOperationException(string.Join(", ", updateResult.Errors.Select(e => e.Description)));

            var userResponse = _mapper.Map<UserResponseDTO>(user);
            userResponse.AccessToken = accessToken;
            userResponse.RefreshToken = refreshToken;
            userResponse.AccessTokenExpiresAt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken).ValidTo;

            return userResponse;
        }

        #endregion

        #region Get User
        public async Task<UserResponseDTO> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting user by id");
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }
            _logger.LogInformation("User found");
            return _mapper.Map<UserResponseDTO>(user);
        }
        public async Task<CurrentUserResponseDTO> GetCurrentUserAsync()
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.GetUserId());
            if (user == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }
            return _mapper.Map<CurrentUserResponseDTO>(user);
        }
        #endregion

        #region Token
        public async Task<CurrentUserResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request)
        {
            _logger.LogInformation("RefreshToken");

            // Hash the incoming RefreshToken and compare it with the one stored in the database
            using var sha256 = SHA256.Create();
            var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.RefreshToken));
            var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

            // Find user based on the refresh token
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
            if (user == null)
            {
                _logger.LogError("Invalid refresh token");
                throw new Exception("Invalid refresh token");
            }

            // Validate the refresh token expiry time
            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                _logger.LogWarning("Refresh token expired for user ID: {UserId}", user.Id);
                throw new Exception("Refresh token expired");
            }

            // Generate a new access token
            var newAccessToken = await _tokenService.GenerateTokenAsync(user);
            _logger.LogInformation("Access token generated successfully");
            var currentUserResponse = _mapper.Map<CurrentUserResponseDTO>(user);
            currentUserResponse.AccessToken = newAccessToken;
            return currentUserResponse;
        }
        public async Task<RevokeRefreshTokenResponseDTO> RevokeRefreshToken(RefreshTokenRequestDTO refreshTokenRemoveRequest)
        {
            _logger.LogInformation("Revoking refresh token");

            try
            {
                // Hash the refresh token
                using var sha256 = SHA256.Create();
                var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshTokenRemoveRequest.RefreshToken));
                var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

                // Find the user based on the refresh token
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
                if (user == null)
                {
                    _logger.LogError("Invalid refresh token");
                    throw new Exception("Invalid refresh token");
                }

                // Validate the refresh token expiry time
                if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    _logger.LogWarning("Refresh token expired for user ID: {UserId}", user.Id);
                    throw new Exception("Refresh token expired");
                }

                // Remove the refresh token
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = null;

                // Update user information in database
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    _logger.LogError("Failed to update user");
                    return new RevokeRefreshTokenResponseDTO
                    {
                        Message = "Failed to revoke refresh token"
                    };
                }
                _logger.LogInformation("Refresh token revoked successfully");
                return new RevokeRefreshTokenResponseDTO
                {
                    Message = "Refresh token revoked successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to revoke refresh token: {ex}", ex.Message);
                throw new Exception("Failed to revoke refresh token");
            }
        }
        #endregion

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                _logger.LogError("User not found");
                throw new Exception("User not found");
            }
            await _userManager.DeleteAsync(user);
        }
        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            _logger.LogInformation("ResetPassword process started");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogError("User with email {Email} not found", request.Email);
                throw new Exception("User not found");
            }

            var newPassword = GenerateRandomPassword();

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to reset password: {errors}", errors);
                throw new Exception($"Failed to reset password: {errors}");
            }

            _logger.LogInformation("Password reset successfully for email: {Email}", request.Email);

            // push new password to email
            var subject = "Ваш новий пароль";
            var body = $"Ваш новий пароль: {newPassword}\nБудь ласка,змініть його після входу в систему.";
            await _emailSender.SendEmailAsync(user.Email, subject, body); // <- отправка письма

            return new ResetPasswordResponseDTO
            {
                Email = user.Email,
                NewPassword = newPassword
            };
        }
        public async Task ConfirmEmailAsync(ConfirmEmailRequestDTO request)
        {
            _logger.LogInformation("Confirming email for userId: {UserId}", request.UserId);

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                _logger.LogError("User not found for confirmation");
                throw new Exception("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to confirm email: {errors}", errors);
                throw new Exception($"Failed to confirm email: {errors}");
            }

            _logger.LogInformation("Email confirmed successfully for userId: {UserId}", request.UserId);
        }
        public async Task<UserResponseDTO> UpdateProfileAsync(UpdateUserRequestDTO request)
        {
            var userId = _currentUserService.GetUserId();
            if (userId == null)
            {
                _logger.LogError("User ID is missing in token");
                throw new ApplicationException("Користувач не автентифікований");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError("User not found with ID: {UserId}", userId);
                throw new ApplicationException("Користувача не знайдено");
            }

            // Обработка смены пароля
            if (!string.IsNullOrEmpty(request.OldPassword) && !string.IsNullOrEmpty(request.NewPassword))
            {
                var passwordIsValid = await _userManager.CheckPasswordAsync(user, request.OldPassword);
                if (!passwordIsValid)
                {
                    _logger.LogWarning("Old password does not match for user {UserId}", userId);
                    throw new ApplicationException("Старий пароль невірний");
                }

                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    _logger.LogError("Password change failed for user {UserId}: {Errors}", userId, string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description)));
                    throw new ApplicationException("Не вдалося змінити пароль: " + string.Join(", ", passwordChangeResult.Errors.Select(e => e.Description)));
                }
            }

            // Обновление полей профиля
            _mapper.Map(request, user);

            user.UpdatedAt = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                _logger.LogError("User update failed for user {UserId}: {Errors}", userId, string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                throw new ApplicationException("Не вдалося оновити профіль: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)));
            }

            _logger.LogInformation("User profile updated successfully for user {UserId}", userId);

            // ✨ Генерируем новый AccessToken
            var newAccessToken = await _tokenService.GenerateTokenAsync(user);

            // ✨ Формируем обновленный UserResponseDTO
            var userResponse = _mapper.Map<UserResponseDTO>(user);
            userResponse.AccessToken = newAccessToken;

            return userResponse;
        }

        #region Private Helpers

        private async Task<string> GenerateUniqueUserNameAsync(string firstName, string lastName)
        {
            var baseUsername = Transliterate(firstName + lastName).ToLower();
            var username = baseUsername;
            int counter = 1;

            while (await _userManager.Users.AnyAsync(u => u.UserName == username))
            {
                username = $"{baseUsername}{counter++}";
            }

            return username;
        }

        private static string HashRefreshToken(string token)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(hash);
        }

        private static string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var password = new StringBuilder();
            using var rng = RandomNumberGenerator.Create();
            var buffer = new byte[sizeof(uint)];

            for (int i = 0; i < 10; i++)
            {
                rng.GetBytes(buffer);
                var num = BitConverter.ToUInt32(buffer, 0);
                password.Append(validChars[(int)(num % (uint)validChars.Length)]);
            }

            return password.ToString();
        }
        private string Transliterate(string input)
        {
            var map = new Dictionary<char, string>
            {
                {'а',"a"}, {'б',"b"}, {'в',"v"}, {'г',"g"}, {'д',"d"}, {'е',"e"}, {'ё',"yo"}, {'ж',"zh"},
                {'з',"z"}, {'и',"i"}, {'й',"y"}, {'к',"k"}, {'л',"l"}, {'м',"m"}, {'н',"n"}, {'о',"o"},
                {'п',"p"}, {'р',"r"}, {'с',"s"}, {'т',"t"}, {'у',"u"}, {'ф',"f"}, {'х',"h"}, {'ц',"ts"},
                {'ч',"ch"}, {'ш',"sh"}, {'щ',"sch"}, {'ъ',""}, {'ы',"y"}, {'ь',""}, {'э',"e"}, {'ю',"yu"}, {'я',"ya"}
            };

            return string.Concat(input.ToLower().Select(c => map.ContainsKey(c) ? map[c] : c.ToString()));
        }

        #endregion
    }
}

