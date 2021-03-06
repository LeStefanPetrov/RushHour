using RushHour.Contracts;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Interfaces
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        string HashPassword(string password);
        Task<UserRoleDto> VerifyUserAsync(string email, string password);
    }
}
