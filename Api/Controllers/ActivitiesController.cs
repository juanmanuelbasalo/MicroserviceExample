using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient busClient;
        public ActivitiesController(IBusClient busClient)
        {
            this.busClient = busClient;
        }
        // GET: api/Activities
        [HttpPost("CreateActivity")]
        public async Task<ActionResult> CreateActivity([FromBody] CreateActivityCommand command)
        {
            var id = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;
            command.Id = id;
            command.CreatedAt = createdAt;

            await busClient.PublishAsync(command);
            return Accepted($"Activities/{command.Id}");
        }
    }
}
