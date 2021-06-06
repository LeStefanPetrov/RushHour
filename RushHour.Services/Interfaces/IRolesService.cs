using RushHour.Contracts;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Interfaces
{
    public interface IRolesService : IBaseService<Role, UserRoleDto>
    {
    }
}
