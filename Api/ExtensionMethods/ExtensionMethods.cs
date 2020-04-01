using Api.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
                => services.AddScoped<IActivityService, ActivityService>();
    }
}
