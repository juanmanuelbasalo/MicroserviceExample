using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public class UserAthenticatedEvent : IEvent
    {
        public string Email { get; }

        public UserAthenticatedEvent(string email)
        {
            Email = email;
        }
    }
}
