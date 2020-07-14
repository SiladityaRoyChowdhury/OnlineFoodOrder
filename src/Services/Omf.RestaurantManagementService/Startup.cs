using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Omf.RestaurantManagementService.Data;
using Omf.RestaurantManagementService.Data.Repository;
using Omf.RestaurantManagementService.Service;
using Microsoft.OpenApi.Models;
using Omf.RestaurantManagementService.EventConsumers;
using MassTransit;

namespace Omf.RestaurantManagementService
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
            services.AddDbContext<RestaurantContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddTransient<IRestaurantRepository, RestaurantRepository>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RestaurantManagement service - Order My Food",
                    Version = "v1"
                });
            });
            services.AddControllers();
            services.AddScoped<OrderConfirmedConsumer>();
            services.AddScoped<UpdateRestaurantConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConfirmedConsumer>();
                x.AddConsumer<UpdateRestaurantConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://rabbitmq/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("OrderConfirmedQueue_" + Guid.NewGuid().ToString(), e => e.Consumer<OrderConfirmedConsumer>(provider));
                cfg.ReceiveEndpoint("RestarantQueue_" + Guid.NewGuid().ToString(), e => e.Consumer<UpdateRestaurantConsumer>(provider));
            }));
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantManagement V1");
            });

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
