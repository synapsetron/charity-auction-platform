
using Microsoft.Extensions.DependencyInjection;
namespace CharityAuction.SignalR
{
    public static class AddSignalRServicesExtension
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();
        }
    }
}
