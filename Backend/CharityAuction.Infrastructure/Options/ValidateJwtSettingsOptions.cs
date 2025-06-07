using Microsoft.Extensions.Options;

namespace CharityAuction.Infrastructure.Options
{
    public class ValidateJwtSettingsOptions : IValidateOptions<JwtSettingsOptions>
    {
        public ValidateOptionsResult Validate(string? name, JwtSettingsOptions options)
        {
            var failures = new List<string>();

            if (string.IsNullOrWhiteSpace(options.Secret))
                failures.Add("JWT Secret is required.");

            if (options.AccessTokenExpirationMinutes < 1 || options.AccessTokenExpirationMinutes > 1440)
                failures.Add("AccessTokenExpirationMinutes must be between 1 and 1440.");

            if (options.RefreshTokenExpirationDays < 1 || options.RefreshTokenExpirationDays > 365)
                failures.Add("RefreshTokenExpirationDays must be between 1 and 365.");

            if (string.IsNullOrWhiteSpace(options.Issuer))
                failures.Add("JWT Issuer is required.");

            if (string.IsNullOrWhiteSpace(options.Audience))
                failures.Add("JWT Audience is required.");

            return failures.Count == 0
                ? ValidateOptionsResult.Success
                : ValidateOptionsResult.Fail(failures);
        }
    }
}
