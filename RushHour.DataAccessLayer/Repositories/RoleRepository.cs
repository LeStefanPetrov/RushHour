using Microsoft.EntityFrameworkCore;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(RushHourContext context) : base(context)
        {
        }

        public async Task<Guid> GetRoleIdAsync(string roleName)
        {
            return await _context.Roles.Where(r =>  r.RoleName == roleName).Select(r => r.ID).FirstAsync();
        }
    }
}
