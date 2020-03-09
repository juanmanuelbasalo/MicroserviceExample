using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Activities.Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> searchTerm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> searchTerm)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
