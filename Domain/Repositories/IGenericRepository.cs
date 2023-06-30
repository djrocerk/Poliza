using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
