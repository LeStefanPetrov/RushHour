using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Guid> GetRoleIdAsync(string roleName);
    }
}
