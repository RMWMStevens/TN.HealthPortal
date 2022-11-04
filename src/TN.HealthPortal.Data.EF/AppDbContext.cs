using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using TN.HealthPortal.Data.EF.Entities;

namespace TN.HealthPortal.Data.EF
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<FarmEntity> Farms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FarmEntity>().HasKey(_ => new { _.BlnNumber, _.PremiseID });

            modelBuilder.Entity<SchemeEntity>().Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            modelBuilder.Entity<DiseaseStatusEntity>().Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            modelBuilder.Entity<ProductEntity>().Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            modelBuilder.Entity<SourceEntity>().Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            modelBuilder.Entity<FarmEntity>()
                .HasMany(farm => farm.Veterinarians)
                .WithMany(vets => vets.Farms)
                .UsingEntity(join => join.ToTable("FarmVeterinarians"));
        }
    }

    internal class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var databaseConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DatabaseConnectionString");
            optionsBuilder.UseSqlServer("Server=tcp:sql-tnhp-a.database.windows.net,1433;Initial Catalog=db-tnhp-a;Persist Security Info=False;User ID=sqladmin;Password=Time4Coffee;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
