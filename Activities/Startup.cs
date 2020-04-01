using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Activities.ExtensionMethods;
using Activities.Handlers;
using AutoMapper;
using Common.Commands;
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

namespace Activities
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
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICommandHandler<CreateActivityCommand>, CreateActivityHandler>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddCustomScoppedServices();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
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
