using Microsoft.Extensions.Options;

namespace CharityAuction.Infrastructure.Options
{
    public class ValidateJwtSettingsOptions : IValidateOptions<JwtSettingsOptions>
    {
        public ValidateOptionsResult Validate(string? name, JwtSettingsOptions options)
        {
            if (string.IsNullOrEmpty(options.Secret))
                return ValidateOptionsResult.Fail("JWT Secret ?? ????? ???? ??????.");

            if (options.AccessTokenExpirationMinutes <= 0)
                return ValidateOptionsResult.Fail("AccessTokenExpirationMinutes ?????? ???? ?????? 0.");

            if (options.RefreshTokenExpirationDays <= 0)
                return ValidateOptionsResult.Fail("RefreshTokenExpirationDays ?????? ???? ?????? 0.");

            if (string.IsNullOrEmpty(options.Issuer))
                return ValidateOptionsResult.Fail("JWT Issuer ?? ????? ???? ??????.");

            if (string.IsNullOrEmpty(options.Audience))
                return ValidateOptionsResult.Fail("JWT Audience ?? ????? ???? ??????.");

            return ValidateOptionsResult.Success;
        }
    }
}

