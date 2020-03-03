using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Commands;
using Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await ServiceHost.CreateHostBuilder<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateActivityCommand>()
                .Build()
                .Run();
        }
    }
}
