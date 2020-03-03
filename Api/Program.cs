using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Events;
using Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await ServiceHost.CreateHostBuilder<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ActivityCreatedEvent>()
                .Build()
                .Run();
        }
    }
}
