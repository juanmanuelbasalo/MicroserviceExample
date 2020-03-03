using Common.Commands;
using Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.RabbitMq
{
    public static class ExtensionMethods
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient busClient, ICommandHandler<TCommand> handler) 
            where TCommand : ICommand
            => busClient.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg), 
                ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient busClient, IEventHandler<TEvent> handler)
           where TEvent : IEvent
           => busClient.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
               ctx => ctx.UseSubscribeConfiguration(cfg => cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static IServiceCollection AddRabbitMq(this IServiceCollection service, IConfiguration configuration)
        {
            var RabbitMq = new RabbitMqOptions();
            var section = configuration.GetSection(nameof(RabbitMq));
            section.Bind(RabbitMq);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = RabbitMq
            });
            return service.AddSingleton<IBusClient>(client);
        }
    }
}
