using Microsoft.EntityFrameworkCore;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly RushHourContext _context;
        public BaseRepository(RushHourContext context)
        {
            _context = context;
        }
      
        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(u => u.ID.Equals(id));
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException();

            if (page > 0 && size > 0)
            {
                return await _context.Set<TEntity>().Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
            }

            else return await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public virtual async Task<bool> DoesEntityExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AnyAsync(filter);
        }
    }
}
