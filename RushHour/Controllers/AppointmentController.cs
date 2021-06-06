using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RushHour.Contracts;
using RushHour.Entities;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RushHour.Cli.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleKeys.AdministratorOrUser)]
    public class AppointmentController : Controller 
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddAppointment([FromBody] AppointmentSpecDto appointmentDto)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            var userRole = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

            if (userRole == RoleKeys.AdministratorRole)
            {
                var entityId = await _appointmentService.InsertAsync(appointmentDto);
                return Created(Request.Path.Value, entityId);
            }
            else
            {
                appointmentDto.UserID = userId;
                var entityId = await _appointmentService.InsertAsync(appointmentDto);
                return Created(Request.Path.Value, entityId);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (!await _appointmentService.DoesEntityExistsAsync(u => u.ID.Equals(appointmentDto.ID)))
                return NotFound();

            var userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            var userRole = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;


            if (userRole == RoleKeys.AdministratorRole)
            {
                await _appointmentService.UpdateAsync(appointmentDto);
                return NoContent();
            }
            if (! await _appointmentService.CheckAppointmentOwnershipAsync(appointmentDto.ID, userId))
               return Forbid();

            appointmentDto.UserID = userId;
            await _appointmentService.UpdateAsync(appointmentDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAppointment([FromBody] Guid appointmentId)
        {
            if (!await _appointmentService.DoesEntityExistsAsync(u => u.ID.Equals(appointmentId)))
                return NotFound();

            var userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            var userRole = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;


            if (userRole == RoleKeys.AdministratorRole)
            {
                await _appointmentService.DeleteAsync(appointmentId);
                return NoContent();
            }

            if (! await _appointmentService.CheckAppointmentOwnershipAsync(appointmentId,userId))
                return Forbid();
            await _appointmentService.DeleteAsync(appointmentId);
            return NoContent();
        }

        [HttpGet("id")]
        public async Task<ActionResult<AppointmentDto>> GetByIdAppointment(Guid appointmentId)
        {
            if (!await _appointmentService.DoesEntityExistsAsync(u => u.ID.Equals(appointmentId)))
                return NotFound();

            var userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            var userRole = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;


            if (userRole == RoleKeys.AdministratorRole)
            {
                return Ok(await _appointmentService.GetById(appointmentId));
            }

            if (!await _appointmentService.CheckAppointmentOwnershipAsync(appointmentId, userId))
               return Forbid();

            return Ok(await _appointmentService.GetById(appointmentId));
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<List<AppointmentDto>>> GetPaginated(int page, int size, Guid? userId)
        {
            var userRole = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            var appointments = new List<AppointmentDto>();

            if (userRole == RoleKeys.AdministratorRole)
            {
                appointments = await _appointmentService.GetPaginatedAppointmentsAsync(page, size, userId);
            }
            else
            {
                userId = Guid.Parse(HttpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
                appointments = await _appointmentService.GetPaginatedAppointmentsAsync(page, size, userId);
            }

            if (!appointments.Any())
                return NotFound();

            return Ok(appointments);
        }
    }
}
