using Microsoft.EntityFrameworkCore;
using TN.HealthPortal.Data.EF.Repositories.Generic;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class VeterinarianRepository : Repository<Veterinarian>, IVeterinarianRepository
    {
        public VeterinarianRepository(AppDbContext context) : base(context)
        {
            var dbSet = context.Set<Veterinarian>();
            dbSet.Include(vet => vet.Farms).ToList();
            dbSet.Include(vet => vet.Countries).ThenInclude(country => country.Region).ToList();
            dbSet.Include(vet => vet.Regions).ToList();
        }
    }
}
