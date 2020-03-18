using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Domain.Services
{
    public interface IUserService
    {
        Task RegisterAsync(User user);
        Task Login(string email, string password);
    }
}
