using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TN.HealthPortal.Lib.Repositories;

// Not yet usable since it needs a translation step between Lib.Models and Data.Entities using AutoMapper

namespace TN.HealthPortal.Data.EF.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> entities;

        public Repository(DbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public TEntity Get(int id)
            => entities.Find(id);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
            => entities.Where(expression);

        public void Add(TEntity entity)
        {
            context.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.RemoveRange(entities);
        }
    }
}
