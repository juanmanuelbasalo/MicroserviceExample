using Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        public Task HandleAsync(CreateUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
