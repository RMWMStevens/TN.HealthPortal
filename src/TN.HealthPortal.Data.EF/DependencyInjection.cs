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
                    //configuration.GetConnectionString("DatabaseConnectionString"),
                    "Server=tcp:sql-tnhp-a.database.windows.net,1433;Initial Catalog=db-tnhp-a;Persist Security Info=False;User ID=sqladmin;Password=Time4Coffee;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            //services.AddScoped<IRepository, Repository>();
            services.AddScoped<ITempFarmRepository, TempFarmRepository>();

            return services;
        }
    }
}
