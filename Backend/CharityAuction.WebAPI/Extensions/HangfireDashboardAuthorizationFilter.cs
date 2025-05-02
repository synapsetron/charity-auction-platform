using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace CharityAuction.WebAPI.Extensions
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Проверяем, что пользователь аутентифицирован
            if (httpContext.User.Identity?.IsAuthenticated != true)
            {
                return false;
            }

            // (Дополнительно можешь тут проверять роль, например "Admin")
            // if (!httpContext.User.IsInRole("Admin"))
            // {
            //     return false;
            // }

            return true;
        }
    }
}
