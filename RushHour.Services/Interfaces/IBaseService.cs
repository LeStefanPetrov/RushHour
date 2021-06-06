using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Interfaces
{
    public interface IBaseService<TEntity, TDto> 
    {
        Task<Guid> InsertAsync<TSpecDto>(TSpecDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(Guid id);
        Task<TDto> GetById(Guid id);
        Task<bool> DoesEntityExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}
