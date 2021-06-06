using AutoMapper;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.DataAccessLayer.Repositories;
using RushHour.Entities;
using RushHour.Services.Interfaces;
using RushHour.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services
{
    public class UsersService : BaseService<User,UserDto>, IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository, IMapper mapper) : base(userRepository,mapper)
        {
            _userRepository = userRepository;
        }

        public override async Task<Guid> InsertAsync<UserDto>(UserDto dto)
        {
            var entity = _mapper.Map<UserDto, User>(dto);
            entity.ID = Guid.NewGuid();
            entity.Password = _userRepository.HashPassword(entity.Password);
            await _userRepository.InsertAsync(entity);
            return entity.ID;
        }

    }
}
