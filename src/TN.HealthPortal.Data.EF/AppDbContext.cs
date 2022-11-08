using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TN.HealthPortal.Lib.Entities;
using TN.HealthPortal.Lib.Entities.Common;

namespace TN.HealthPortal.Data.EF
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Farm> Farms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetPrimaryKeys(modelBuilder);
            SetTableNames(modelBuilder);
            SetJoiningTableNames(modelBuilder);
        }

        private void SetPrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>().HasKey(_ => _.Id);
            modelBuilder.Entity<Manufacturer>().HasKey(_ => _.Name);
            modelBuilder.Entity<Pathogen>().HasKey(_ => _.Name);
            modelBuilder.Entity<Source>().HasKey(_ => _.FarmId);
            modelBuilder.Entity<Veterinarian>().HasKey(_ => _.Id);
        }

        private void SetTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>(_ => _.ToTable("Farms"));
            modelBuilder.Entity<DewormingScheme>(_ => _.ToTable("DewormingSchemes"));
            modelBuilder.Entity<Source>(_ => _.ToTable("Sources"));
            modelBuilder.Entity<Veterinarian>(_ => _.ToTable("Veterinarians"));
            modelBuilder.Entity<DiseaseStatus>(_ => _.ToTable("DiseaseStatuses"));
            modelBuilder.Entity<Manufacturer>(_ => _.ToTable("Manufacturers"));
            modelBuilder.Entity<Product>(_ => _.ToTable("Products"));
            modelBuilder.Entity<Scheme>(_ => _.ToTable("Schemes"));
            modelBuilder.Entity<DewormingScheme>(_ => _.ToTable("DewormingSchemes"));
            modelBuilder.Entity<VaccinationScheme>(_ => _.ToTable("VaccinationSchemes"));
            modelBuilder.Entity<Pathogen>(_ => _.ToTable("Pathogens"));
        }

        private void SetJoiningTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>()
                .HasMany(farm => farm.Veterinarians)
                .WithMany(vets => vets.Farms)
                .UsingEntity(join => join.ToTable("FarmVeterinarians"));
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).CreatedAt = now;
                }
                ((Entity)entity.Entity).UpdatedAt = now;
            }
        }
    }

    internal class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var databaseConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DatabaseConnectionString");
            optionsBuilder.UseSqlServer(databaseConnectionString);
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
