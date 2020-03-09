using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Activities.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity Find(Expression<Func<TEntity, bool>> searchTerm);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> searchTerm);
        Task<bool> SaveAsync();
    }
}
