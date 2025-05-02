using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class JwtSettingsOptions
    {
        public const string SectionName = "JwtSettings";

        [Required]
        public string Secret { get; set; } = string.Empty;

        [Required]
        [Range(1, 1440, ErrorMessage = "AccessTokenExpirationMinutes must be in range from 1 to 1440 minutes.")]
        public int AccessTokenExpirationMinutes { get; set; }

        [Required]
        [Range(1, 365, ErrorMessage = "RefreshTokenExpirationDays must be in range from  1 ?? 365 days.")]
        public int RefreshTokenExpirationDays { get; set; }

        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;
    }
}

