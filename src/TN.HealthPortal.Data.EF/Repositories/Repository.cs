﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TN.HealthPortal.Logic.Repositories;

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

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, string[]? includes = null)
        {
            var query = entities;

            foreach (var include in includes ?? Array.Empty<string>())
            {
                query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }

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
