using AutoMapper;
using Common.Commands;
using Common.Events;
using Common.Exceptions;
using Identity.Domain.Entities;
using Identity.Domain.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IBusClient busClient;
        private readonly IUserService userService;
        private readonly ILogger<CreateUserHandler> logger;
        private readonly IMapper mapper;

        public CreateUserHandler(IBusClient busClient, IUserService userService, 
            ILogger<CreateUserHandler> logger, IMapper mapper)
        {
            this.busClient = busClient;
            this.userService = userService;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task HandleAsync(CreateUserCommand command)
        {
            logger.LogInformation($"Creating User: {command.Name}");
            try
            {
                var user = mapper.Map<User>(command);
                await userService.RegisterAsync(user);

                await busClient.PublishAsync(new UserCreatedEvent(user.Email,user.Name));
            }
            catch (CustomException ex)
            {
                await busClient.PublishAsync(new CreateUserRejectedEvent(command.Email, "error", ex.Code));
                logger.LogError(ex.Message);
            }
        }
    }
}
