using CharityAuction.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CharityAuction.WebAPI.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            try
            {
                var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await applicationDbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during startup migration");
            }
        }
    }
}

