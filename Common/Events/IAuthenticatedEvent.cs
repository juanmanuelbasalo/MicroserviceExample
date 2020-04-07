using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
         Guid UserId { get; }
    }
}
