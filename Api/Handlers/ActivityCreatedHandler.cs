using Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreatedEvent>
    {
        public async Task HandleAsync(ActivityCreatedEvent @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
