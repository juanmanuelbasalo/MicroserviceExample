using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Mongo
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection service, IConfiguration configuration)
        {
            return service.Configure<MongoOptions>(configuration.GetSection("mongo"))
                 .AddSingleton(c =>
                 {
                     var options = c.GetService<IOptions<MongoOptions>>();
                     return new MongoClient(options.Value.ConnectionString);
                 })
                 .AddScoped(c =>
                 {
                     var options = c.GetService<IOptions<MongoOptions>>();
                     var client = c.GetService<MongoClient>();

                     return client.GetDatabase(options.Value.Database);
                 })
                 .AddScoped<IDatabaseInitializer, MongoInitializer>()
                 .AddScoped<IDatabaseSeeder, MongoSeeder>();
        }
    }
}
