using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Domain.Entities;
using System.Security.Claims;


namespace CharityAuction.Application.Services.User.JWT
{
    public class ClaimsService : IClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClaimsService> _logger;

        public ClaimsService(UserManager<ApplicationUser> userManager, ILogger<ClaimsService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<Claim>> CreateClaimsAsync(ApplicationUser user)
        {
            if (user == null)
            {
                _logger.LogError("User cannot be null.");
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.UserName ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user?.Id ?? string.Empty),
                new Claim(ClaimTypes.Email, user?.Email ?? string.Empty),
                new Claim("FirstName", user?.FirstName ?? string.Empty),
                new Claim("LastName", user?.LastName ?? string.Empty),
                new Claim(ClaimTypes.Role,user?.Role ?? string.Empty)
            };

            try
            {
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user roles.");
                throw new InvalidOperationException("An error occurred while retrieving user roles.", ex);
            }

            return claims;
        }
    }
}

