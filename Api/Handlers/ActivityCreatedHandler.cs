using Api.Domain.Entities;
using Api.Domain.Services;
using AutoMapper;
using Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreatedEvent>
    {
        private readonly IActivityService activityService;

        public ActivityCreatedHandler(IActivityService activityService)
        {
            this.activityService = activityService;
        }
        public async Task HandleAsync(ActivityCreatedEvent @event)
        {
            await activityService.AddAsync(@event);
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
