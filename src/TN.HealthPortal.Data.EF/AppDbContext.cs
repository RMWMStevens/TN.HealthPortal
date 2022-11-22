using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Entities.Common;

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
            base.OnModelCreating(modelBuilder);
            SetPrimaryKeys(modelBuilder);
            SetTableNames(modelBuilder);
        }

        private void SetPrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>().HasKey(_ => _.BlnNumber);
            modelBuilder.Entity<Veterinarian>().HasKey(_ => _.EmployeeCode);
            modelBuilder.Entity<Manufacturer>().HasKey(_ => _.Name);
            modelBuilder.Entity<Pathogen>().HasKey(_ => _.Name);
            modelBuilder.Entity<Address>().HasKey(_ => _.FarmBlnNumber);
            modelBuilder.Entity<Country>().HasKey(_ => _.Name);
            modelBuilder.Entity<ProductionType>().HasKey(_ => new { _.Name, _.FarmBlnNumber });
            modelBuilder.Entity<Region>().HasKey(_ => _.Name);
            modelBuilder.Entity<Source>().HasKey(_ => new { _.FarmBlnNumber, _.Category });
            modelBuilder.Entity<DiseaseStatus>().HasKey(_ => new { _.FarmBlnNumber, _.Disease });
            modelBuilder.Entity<Product>().HasKey(_ => new { _.Name });
        }

        private void SetTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>().ToTable("Farms");
            modelBuilder.Entity<Veterinarian>().ToTable("Veterinarians");
            modelBuilder.Entity<Source>().ToTable("Sources");
            modelBuilder.Entity<DiseaseStatus>().ToTable("DiseaseStatuses");
            modelBuilder.Entity<Manufacturer>().ToTable("Manufacturers");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<DewormingScheme>().ToTable("DewormingSchemes");
            modelBuilder.Entity<VaccinationScheme>().ToTable("VaccinationSchemes");
            modelBuilder.Entity<Pathogen>().ToTable("Pathogens");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<ProductionType>().ToTable("ProductionTypes");

            modelBuilder.Entity<Farm>()
                .HasMany(farm => farm.Veterinarians)
                .WithMany(vets => vets.Farms)
                .UsingEntity(join => join.ToTable("FarmVeterinarians"));
            modelBuilder.Entity<Veterinarian>()
                .HasMany(vet => vet.Countries)
                .WithMany(countries => countries.Veterinarians)
                .UsingEntity(join => join.ToTable("CountryVeterinarians"));
            modelBuilder.Entity<Veterinarian>()
                .HasMany(vet => vet.Regions)
                .WithMany(regions => regions.Veterinarians)
                .UsingEntity(join => join.ToTable("RegionVeterinarians"));
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).SetCreatedAndUpdated(new Veterinarian { EmployeeCode = "123456" }); // TODO: Set veterinarian from session
                }
                else
                {
                    ((Entity)entity.Entity).SetUpdated(new Veterinarian { EmployeeCode = "123456" }); // TODO: Set veterinarian from session
                }
            }
        }
    }

    internal class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
