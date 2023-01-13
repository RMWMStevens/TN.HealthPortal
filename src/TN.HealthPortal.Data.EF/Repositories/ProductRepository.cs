using TN.HealthPortal.Data.EF.Repositories.Generic;
using TN.HealthPortal.Logic.Entities;
using TN.HealthPortal.Logic.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
