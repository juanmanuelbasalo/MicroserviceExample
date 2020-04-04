using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Services;
using Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient busClient;
        private readonly IActivityService activityService;

        public ActivitiesController(IBusClient busClient, IActivityService activityService)
        {
            this.busClient = busClient;
            this.activityService = activityService;
        }

        [HttpPost("CreateActivity")]
        public async Task<ActionResult> CreateActivity([FromBody] CreateActivityCommand command)
        {
            var id = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;
            command.Id = id;
            command.CreatedAt = createdAt;
            command.UserId = Guid.Parse(User.Identity.Name);
            await busClient.PublishAsync(command);
            return Accepted($"Activities/{command.Id}");
        }

        [HttpGet("GetActivity")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Get()
        {
            var activities = await activityService.GetAllAsync(x => x.Id.Equals(Guid.Parse(User.Identity.Name)));

            return new JsonResult(activities.Select(x => new { x.Id, x.Name, x.Category, x.CreatedAt }));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Activity>> Get(Guid id)
        {
            var activity = await activityService.GetAsync(x => x.Id.Equals(id));
            if (activity == null) return NotFound();

            if (activity.Id != Guid.Parse(User.Identity.Name)) return Unauthorized();

            return activity;
        }
    }
}
