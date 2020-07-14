using AutoMapper;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Omf.OrderManagementService.Data;
using Omf.OrderManagementService.Data.Repository;
using Omf.OrderManagementService.Services;
using System;
using Omf.OrderManagementService.EventConsumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Omf.OrderManagementService
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
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderService, OrderService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "OrderManagement service - Order My Food",
                    Version = "v1"
                });
            });
            services.AddControllers();

            //var bus = Bus.Factory.CreateUsingRabbitMq(rmq =>
            //{
            //    var host = rmq.Host(new Uri("rabbitmq://rabbitmq"), "/", h =>
            //    {
            //        h.Username("guest");
            //        h.Password("guest");
            //    });
            //    rmq.ReceiveEndpoint(host, "Order" + Guid.NewGuid().ToString(), e =>
            //    {
            //        e.LoadFrom();
            //        //e.Consumer<PaymentEventConsumer>(services);

            //    });
            //    //rmq.ReceiveEndpoint(host, "PaymentQueue_" + Guid.NewGuid().ToString(), e => e.Consumer<PaymentEventConsumer>());
            //    //rmq.ReceiveEndpoint(host, "DeliverytQueue_" + Guid.NewGuid().ToString(), e => e.Consumer<DeliveryEventConsumer>());

            //});
            services.AddScoped<PaymentEventConsumer>();
            services.AddScoped<DeliveryEventConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PaymentEventConsumer>();
                x.AddConsumer<DeliveryEventConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://rabbitmq/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                cfg.ReceiveEndpoint("PaymentQueue_" + Guid.NewGuid().ToString(), e => e.Consumer<PaymentEventConsumer>(provider));
                cfg.ReceiveEndpoint("DeliverytQueue_" + Guid.NewGuid().ToString(), e => e.Consumer<DeliveryEventConsumer>(provider));
            }));
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            //services.AddSingleton<IPublishEndpoint>(bus);
            //services.AddSingleton<ISendEndpointProvider>(bus);
            //services.AddSingleton<IBus>(bus);
            //services.AddSingleton<IBusControl>(bus);

            //services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManagement V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //var bus = ApplicationContainer.Resolve<IBusControl>();
            //var busHandle = TaskUtil.Await(() => bus.StartAsync());
            //lifeTime.ApplicationStopping.Register(() => busHandle.Stop());
        }
    }
}
