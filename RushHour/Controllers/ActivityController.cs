using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RushHour.Contracts;
using RushHour.Entities;
using RushHour.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RushHour.Cli.Controllers
{
    [Route("api/activity")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleKeys.AdministratorRole)]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddActivity([FromBody] ActivitySpecDto activityDto)
        {
            var entityId = await _activityService.InsertAsync(activityDto);
            return Created(Request.Path.Value,entityId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateActivity([FromBody] ActivityDto activityDto)
        {
            Expression<Func<Activity, bool>> filter = u => u.ID.Equals(activityDto.ID);
            if (! await _activityService.DoesEntityExistsAsync(filter))
                return NotFound();

            await _activityService.UpdateAsync(activityDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteActivity([FromBody] Guid id)
        {
            Expression<Func<Activity, bool>> filter = u => u.ID.Equals(id);
            if (! await _activityService.DoesEntityExistsAsync(filter))
                return NotFound();

            await _activityService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("id")]
        public async Task<ActionResult<ActivityDto>> GetByIdActivity(Guid id)
        {
            var activity = await _activityService.GetById(id);

            if (activity == null)
                return NotFound();

            return Ok(activity);
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<ActivityDto>> GetPaginated(int page, int size, string name)
        {
            var activities = await _activityService.GetPaginatedActivitiesAsync(page, size, name);

            if (!activities.Any())
                return NotFound();

            return Ok(activities);
        }
    }
}
