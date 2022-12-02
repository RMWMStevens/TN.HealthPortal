using System.Linq.Expressions;

namespace TN.HealthPortal.Logic.Repositories.Generic
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, string[]? includes = null);

        Task RemoveAsync(TEntity entity);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}
