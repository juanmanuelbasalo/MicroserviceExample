using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Activities.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> searchTerm);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> searchTerm);
        Task<bool> SaveAsync();
    }
}
