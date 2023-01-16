using Microsoft.Extensions.DependencyInjection;
using TN.HealthPortal.Logic.Generators;
using TN.HealthPortal.Logic.Services;

namespace TN.HealthPortal.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IDewormingSchemeService, DewormingSchemeService>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<IVaccinationSchemeService, VaccinationSchemeService>();
            services.AddScoped<IVeterinarianService, VeterinarianService>();
            services.AddScoped<IFarmExportGenerator, FarmToPdfExportGenerator>();

            return services;
        }
    }
}