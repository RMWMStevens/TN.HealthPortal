using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TN.HealthPortal.Data.EF.Repositories;
using TN.HealthPortal.Lib.Repositories;

namespace TN.HealthPortal.Data.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseConnectionString"),
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IFarmRepository, FarmRepository>();

            return services;
        }
    }
}
