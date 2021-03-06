﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Auth;
using Common.Exceptions;
using Common.Repositories;
using Identity.Domain.Entities;

namespace Identity.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> repository;
        private readonly IJwtHandler jwtHandler;

        public UserService(IMongoRepository<User> repository, IJwtHandler jwtHandler)
        {
            this.repository = repository;
            this.jwtHandler = jwtHandler;
        }
        public async Task<CustomJsonWebToken> LoginAsync(string email, string password)
        {
            var user = await repository.FindAsync(u => u.Email.Equals(email)) ?? 
                throw new CustomException("invalid_credentials", $"Invalid credentials");

            if (!user.ValidPassword(password))
            {
                throw new CustomException("invalid_credentials", $"Invalid credentials");
            }

            return jwtHandler.Create(user.Id);
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
