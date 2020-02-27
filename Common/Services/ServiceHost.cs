using Common.Commands;
using Common.Events;
using Common.RabbitMq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IHost host;

        public ServiceHost(IHost host) => this.host = host;
        public async Task Run() => await host.RunAsync();

        public static IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : class =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });

        private abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }
        private class HostBuilder : BuilderBase
        {
            private readonly IHost host;
            private IBusClient busClient;

            public HostBuilder(IHost host)
            {
                this.host = host;
            }

            public BusBuilder UseRabbitMq()
            {
                busClient = (IBusClient)host.Services.GetService(typeof(IBusClient));
                return new BusBuilder(host, busClient);
            }
            public override ServiceHost Build()
            {
                return new ServiceHost(host);
            }
        }

        private class BusBuilder : BuilderBase
        {
            private readonly IHost host;
            private IBusClient busClient;

            public BusBuilder(IHost host, IBusClient busClient)
            {
                this.host = host;
                this.busClient = busClient;
            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var handler = (ICommandHandler<TCommand>)host.Services
                    .GetService(typeof(ICommandHandler<TCommand>));

                busClient.WithCommandHandlerAsync(handler);

                return this;
            }

            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var handler = (IEventHandler<TEvent>)host.Services
                    .GetService(typeof(IEventHandler<TEvent>));

                busClient.WithEventHandlerAsync(handler);

                return this;
            }

            public override ServiceHost Build()
            {
                throw new NotImplementedException();
            }
        }
    }
}
