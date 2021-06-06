using AutoMapper;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Services
{
    public class RolesService : BaseService<Role, UserRoleDto>, IRolesService
    {
        private readonly IRoleRepository _roleRepository;
        public RolesService(IRoleRepository roleRepository,IMapper mapper) : base(roleRepository, mapper)
        {
            _roleRepository = roleRepository;
        }

    }
}
