using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Exceptions;
using Identity.Domain.Entities;
using Identity.Domain.Repositories;

namespace Identity.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }
        public async Task Login(string email, string password)
        {
            var user = await repository.FindAsync(u => u.Equals(email)) ?? 
                throw new CustomException("invalid_credentials", $"Invalid credentials");

            if (!user.ValidPassword(password))
            {
                throw new CustomException("invalid_credentials", $"Invalid credentials");
            }
        }

        public async Task RegisterAsync(User user)
        {
            var existingUser = await repository.FindAsync(u => user.Email.Equals(u.Email));

            if (existingUser != null) throw new CustomException("email_in_use", $"Email: {user.Email} is already taken.");


            var nonHashedPass = user.Password;
            user.SetPasswordToHashed(nonHashedPass);

            await repository.InsertAsync(user);
        }
    }
}
