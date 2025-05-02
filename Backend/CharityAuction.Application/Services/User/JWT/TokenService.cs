
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace CharityAuction.Application.Services.User.JWT
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettingsOptions _jwtConfiguration;
        private readonly IClaimsService _claimsService;
        private readonly ILogger<TokenService> _logger;


        public TokenService(IOptions<JwtSettingsOptions> options, IClaimsService claimsService, ILogger<TokenService> logger)
        {
            _jwtConfiguration = options.Value ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(_jwtConfiguration.Secret))
            {
                throw new InvalidOperationException("JWT Secret key is missing in configuration.");
            }

            _claimsService = claimsService;
            _logger = logger;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            if (user == null)
            {
                _logger.LogError("User cannot be null.");
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            var claims = await _claimsService.CreateClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtConfiguration.AccessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

