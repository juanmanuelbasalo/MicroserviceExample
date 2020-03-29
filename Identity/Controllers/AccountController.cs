using Common.Auth;
using Common.Commands;
using Identity.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: api/Activities
        [HttpPost("login")]
        public async Task<ActionResult<CustomJsonWebToken>> Login([FromBody] AuthenticateUserCommand command)
        {
            var token = await userService.LoginAsync(command.Email, command.Password);
            return token;
        }
    }
}
