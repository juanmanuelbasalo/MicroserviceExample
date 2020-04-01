using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ExtensionMethods;
using Api.Handlers;
using AutoMapper;
using Common.Auth;
using Common.Events;
using Common.Mongo;
using Common.RabbitMq;
using Common.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddMongoDb(Configuration);
            services.AddScoped<IEventHandler<ActivityCreatedEvent>, ActivityCreatedHandler>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScopedServices();
            services.AddControllers();
            services.AddJwt(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
