using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
         Task InsertAsync(TEntity entity);
         Task UpdateAsync(TEntity entity);
         Task DeleteAsync(Guid id);
         Task<TEntity> GetByIdAsync(Guid id);
         Task<IEnumerable<TEntity>> GetPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter);
         Task<bool> DoesEntityExistAsync(Expression<Func<TEntity, bool>> filter);
    }
}
