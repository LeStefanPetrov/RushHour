using AutoMapper;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.DataAccessLayer.Repositories;
using RushHour.Entities;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Services
{
    public class BaseService<TEntity,TDto> : IBaseService<TEntity, TDto> where TEntity : BaseEntity where TDto : BaseDto
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<Guid> InsertAsync<TSpecDto>(TSpecDto dto)
        {
            var entity = _mapper.Map<TSpecDto, TEntity>(dto);
            await _baseRepository.InsertAsync(entity);
            return entity.ID;
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TDto, TEntity>(dto);
            await _baseRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public virtual async Task<TDto> GetById(Guid id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            var dto = _mapper.Map<TEntity,TDto>(entity);
            return dto;
        }

        public virtual async Task<bool> DoesEntityExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _baseRepository.DoesEntityExistAsync(filter);
        }

    }
}
