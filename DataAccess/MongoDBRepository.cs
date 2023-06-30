using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DataAccess.MongoDB
{
    public abstract class MongoDBRepository<TEntity, TId>
        : IGenericRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly IMongoCollection<TEntity> _collection;
        public abstract string COLLECTION_NAME { get; }

        public MongoDBRepository(IMongoDatabase mongoDatabase)
            => _collection = mongoDatabase.GetCollection<TEntity>(COLLECTION_NAME);

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
            => _collection.CountDocumentsAsync(predicate);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return await _collection.Find(m => m.Id!.Equals(entity.Id)).FirstOrDefaultAsync();
        }

        public Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
            => _collection.Find(predicate).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _collection.Find(predicate).ToListAsync();
    }
}
