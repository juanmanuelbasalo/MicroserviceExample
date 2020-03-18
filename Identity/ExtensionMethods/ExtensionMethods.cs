using Common.Mongo;
using Identity.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddCustomScoppedServices(this IServiceCollection service)
            => service.AddScoped<IUserService, UserService>()
         
    }
}
