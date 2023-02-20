using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Service.Context;
using Order.Service.Filters;
using Order.Service.Services;

namespace Order.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new GeneralExceptionHandler());
            });

            builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["CustomerServiceUrl"]);
            });
            // builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1.0.0", new OpenApiInfo
                {
                    Title = "Order Service Documentation",
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1.0.0/swagger.json", "Order Service");
                });
            }

            app.MapControllers();

            app.Run();
        }
    }
}