using CharityAuction.Application.Interfaces;
using CharityAuction.WebAPI.Extensions;
using CharityAuction.WebAPI.Hubs;
using Hangfire;
using Serilog;
namespace CharityAuction
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddUserSecrets<Program>();
            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddSwaggerServices();
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddCustomServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddAuthServices(builder.Configuration);
            builder.Services.AddCorsPolicies();
            builder.Services.AddControllers();
            builder.Services.AddAuthentication();
            builder.Services.AddHangfireServices(builder.Configuration);

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CharityAuction API v1");
                });
            }
            await app.ApplyMigrations();

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
            });
            app.MapControllers();

            app.MapHub<AuctionHub>("/auctionHub");

            RecurringJob.AddOrUpdate<IAuctionClosingService>(
                        "close-expired-auctions",
                        service => service.CloseExpiredAuctionsAsync(),
                        Cron.Minutely // ????? ????? ????????
                    );

            app.Run();
        }
    }
}
