using Microsoft.Extensions.DependencyInjection;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IFarmService, FarmService>();

            return services;
        }
    }
}
