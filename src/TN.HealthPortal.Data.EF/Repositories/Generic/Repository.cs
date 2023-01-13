using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Repositories.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TN.HealthPortal.Data.EF.Tests")]

namespace TN.HealthPortal.Data.EF.Repositories.Generic
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<TEntity> entities;

        public Repository(AppDbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await entities.Where(predicate).ToListAsync();

        public async Task RemoveAsync(TEntity entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            context.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
