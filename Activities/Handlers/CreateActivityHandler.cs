using Activities.Domain.Entities;
using Activities.Domain.Services;
using AutoMapper;
using Common.Commands;
using Common.Events;
using Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivityCommand>
    {
        private readonly IBusClient busClient;
        private readonly IActivityService activityService;
        private readonly IMapper mapper;
        private readonly ILogger<CreateActivityHandler> logger;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService
                                    ,IMapper mapper, ILogger<CreateActivityHandler> logger)
        {
            this.busClient = busClient;
            this.activityService = activityService;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task HandleAsync(CreateActivityCommand command)
        {
            logger.LogInformation($"Creating Activity: {command.Name}");
            try
            {
                var activity = mapper.Map<Activity>(command);
                await activityService.AddAsync(activity);

                await busClient.PublishAsync(new ActivityCreatedEvent(command.Id, command.UserId, command.Category, command.Name, command.Description
                                            ,command.CreatedBy, command.CreatedAt));
            }
            catch (CustomException ex)
            {
                await busClient.PublishAsync(new CreateActivityRejectedEvent(command.Id, ex.Code, ex.Message));
                logger.LogError(ex.Message);
            }
        }
    }
}
