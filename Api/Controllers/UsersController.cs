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
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        private readonly IBusClient busClient;
        public UsersController(IBusClient busClient)
        {
            this.busClient = busClient;
        }
        // GET: api/Activities
        [HttpPost("RegisterUser")]
        public async Task<ActionResult> CreateActivity([FromBody] CreateUserCommand command)
        {   
            await busClient.PublishAsync(command);
            return Accepted();
        }
    }
}
