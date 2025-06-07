using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class JwtSettingsOptions
    {
        public const string SectionName = "JwtSettings";

        [Required(ErrorMessage = "JWT Secret is required.")]
        public string Secret { get; set; } = string.Empty;

        [Required(ErrorMessage = "AccessTokenExpirationMinutes is required.")]
        [Range(1, 1440, ErrorMessage = "AccessTokenExpirationMinutes must be in range from 1 to 1440 minutes.")]
        public int AccessTokenExpirationMinutes { get; set; }

        [Required(ErrorMessage = "RefreshTokenExpirationDays is required.")]
        [Range(1, 365, ErrorMessage = "RefreshTokenExpirationDays must be in range from 1 to 365 days.")]
        public int RefreshTokenExpirationDays { get; set; }

        [Required(ErrorMessage = "JWT Issuer is required.")]
        public string Issuer { get; set; } = string.Empty;

        [Required(ErrorMessage = "JWT Audience is required.")]
        public string Audience { get; set; } = string.Empty;
    }
}
