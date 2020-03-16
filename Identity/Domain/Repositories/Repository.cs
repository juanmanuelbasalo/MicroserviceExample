using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace Identity.Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IMongoDatabase mongoDatabase;
        private IMongoCollection<TEntity> Collection => mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
        public Repository(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Collection.AsQueryable().ToListAsync();

        public async Task InsertAsync(TEntity entity) => await Collection.InsertOneAsync(entity);

        public async Task DeleteAsync(TEntity entity) => await Collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("i", entity));

        public async Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> searchTerm)
            => await Collection.AsQueryable().FirstOrDefaultAsync(searchTerm);

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> searchTerm)
            => await Collection.AsQueryable().Where(searchTerm).ToListAsync();

        public async Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
