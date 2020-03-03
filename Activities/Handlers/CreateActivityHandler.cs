using Common.Commands;
using Common.Events;
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

        public CreateActivityHandler(IBusClient busClient)
        {
            this.busClient = busClient;
        }
        public async Task HandleAsync(CreateActivityCommand command)
        {
            Console.WriteLine($"Creating Activity: {command.Name}");
            await busClient.PublishAsync(new ActivityCreatedEvent(command.Id, command.UserId,command.Category,command.Name,command.Description
                ,command.CreatedBy,command.CreatedAt));
        }
    }
}
