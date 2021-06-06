using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RushHourContext context) : base(context)
        {
        }

        public string HashPassword(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }
        

        public async Task<UserRoleDto> VerifyUserAsync(string email, string password)
        {
            var user = await _context.Users.Include(r => r.Role)
                .Where(u => u.Email == email && u.Password.Equals(HashPassword(password)))
                .Select(r => new UserRoleDto{ ID = r.ID, RoleName = r.Role.RoleName })
                .FirstOrDefaultAsync();
            return user;
        }

    }
}
