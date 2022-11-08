using Microsoft.Extensions.DependencyInjection;
using TN.HealthPortal.Lib.Services;

namespace TN.HealthPortal.Lib
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
