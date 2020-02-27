using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Events
{
    public class CreateUserRejectedEvent : IRejectedEvent
    {
        public string Email { get; }
        public string Reason { get; }

        public string Code { get; }

        public CreateUserRejectedEvent(string email, string reason, string code)
        {
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}
