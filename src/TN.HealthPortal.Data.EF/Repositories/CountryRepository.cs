using TN.HealthPortal.Data.EF.Repositories.Generic;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext context) : base(context) { }
    }
}
