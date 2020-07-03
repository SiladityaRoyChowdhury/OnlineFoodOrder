using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Omf.OrderManagementService.Data;
using Omf.OrderManagementService.Data.Repository;
using Omf.OrderManagementService.Services;

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
            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "OrderManagement service - Order My Food",
                    Version = "v1"
                });
            });
            services.AddControllers();
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
        }
    }
}
