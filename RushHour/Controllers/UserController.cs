using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RushHour.Contracts;
using RushHour.DataAccessLayer.Infrastructure;
using RushHour.DataAccessLayer.Interfaces;
using RushHour.DataAccessLayer.Repositories;
using RushHour.Entities;
using RushHour.Services;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RushHour.Cli.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;
        public UserController(IUsersService usersService, IRolesService rolesService)
        {
            _usersService = usersService;
            _rolesService = rolesService;
        }

        [HttpPost("registration")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Registration(UserDto user)
        {
            user.RoleName = "User";
            return Created(Request.Path.Value, await _usersService.InsertAsync(user));
        }

        [HttpPost("adminRegistration")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleKeys.AdministratorRole)]
        public async Task<ActionResult<string>> AdminRegistration(UserDto user)
        {
            Expression<Func<Role, bool>> filter = u => u.RoleName == user.RoleName;
            if (! await _rolesService.DoesEntityExistsAsync(filter))
                return NotFound();

            return Created(Request.Path.Value, await _usersService.InsertAsync(user));
        }
    }
}
