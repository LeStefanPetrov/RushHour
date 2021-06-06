using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RushHour.Services;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Cli.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string email, string password)
        {
            var token = await _loginService.GenerateJwtTokenAsync(email, password);

            if (token == null)
                return BadRequest();

            return Ok(token);
        }

    }
}
