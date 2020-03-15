using Activities.Domain.Services;
using Activities.MongoServices;
using Common.Mongo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activities.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddCustomScoppedServices(this IServiceCollection service)
            => service.AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IActivityService, ActivityService>()
                .AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
    }
}
