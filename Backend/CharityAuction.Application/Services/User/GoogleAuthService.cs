using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using CharityAuction.Domain.Entities;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.DTO.User;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ILogger<GoogleAuthService> _logger;

    public GoogleAuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, ILogger<GoogleAuthService> logger)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<UserResponseDTO> LoginWithGoogleAsync(string idToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = payload.Email,
                Email = payload.Email,
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                PhotoUrl = payload.Picture ?? "https://cdn-icons-png.flaticon.com/512/2202/2202112.png",
                EmailConfirmed = true, 
                Role = UserRole.Seller,
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to create user from Google login: {errors}", errors);
                throw new Exception($"Failed to create user: {errors}");
            }
        }

        var accessToken = await _tokenService.GenerateTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(2);

        await _userManager.UpdateAsync(user);

        return new UserResponseDTO
        {
            Id = Guid.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhotoUrl = user.PhotoUrl,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
