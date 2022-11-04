using Microsoft.Extensions.DependencyInjection;

namespace TN.HealthPortal.Lib
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogicLayer(this IServiceCollection services)
        {
            //services.AddScoped<IService, Service>();

            return services;
        }
    }
}
