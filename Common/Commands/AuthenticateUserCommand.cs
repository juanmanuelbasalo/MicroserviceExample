using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Commands
{
    public class AuthenticateUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
