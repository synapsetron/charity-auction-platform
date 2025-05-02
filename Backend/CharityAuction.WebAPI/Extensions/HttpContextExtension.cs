using CharityAuction.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace CharityAuction.WebAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static void AppendTokenToCookie(this HttpContext context, string token, IOptions<JwtSettingsOptions> options)
        {
            var _jwtConfiguration = options.Value;

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(_jwtConfiguration.AccessTokenExpirationMinutes)
            };

            context.Response.Cookies.Append("AuthToken", token, cookieOptions);
        }
        public static void DeleteAuthTokenCookie(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.Cookies.Delete("AuthToken");
        }
    }
}
