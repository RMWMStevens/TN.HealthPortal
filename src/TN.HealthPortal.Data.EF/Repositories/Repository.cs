using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TN.HealthPortal.Lib.Repositories;

namespace TN.HealthPortal.Data.EF.Repositories
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

        public void Add(TEntity entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.AddRange(this.entities);
            context.SaveChanges();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public void Remove(TEntity entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.RemoveRange(this.entities);
            context.SaveChanges();
        }
    }
}
