using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivityCommand>
    {
        public async Task HandleAsync(CreateActivityCommand command)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Create Activity: {command.Name}");
        }
    }
}
