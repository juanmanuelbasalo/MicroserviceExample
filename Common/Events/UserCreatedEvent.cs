using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public class UserCreatedEvent : IEvent
    {
        public  string Email { get; }
        public string Name { get; }

        public UserCreatedEvent(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
