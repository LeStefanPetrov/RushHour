using Microsoft.IdentityModel.Tokens;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.DataAccessLayer.Repositories;
using RushHour.Entities;
using RushHour.Infrastructure;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtSettings _settings;
        private readonly IUserRepository _userRepository;
        public LoginService(JwtSettings settings, IUserRepository userRepository)
        {
            _settings = settings;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateJwtTokenAsync(string email, string password)
        {
            var user = await _userRepository.VerifyUserAsync(email, password);

            if (user != null)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub,email),
                new Claim(JwtRegisteredClaimNames.Jti, user.ID.ToString())
                };

                claims.Add(new Claim(ClaimTypes.Role, user.RoleName));

                var token = new JwtSecurityToken(
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else return null;
        }
    }
}
