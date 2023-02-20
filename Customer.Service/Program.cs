using Customer.Service.Context;
using Customer.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Service.Filters;

namespace Customer.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Development"));
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new GeneralExceptionHandler());
            });

            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1.0.0", new OpenApiInfo
                {
                    Title = "Customer Service Documentation",
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1.0.0/swagger.json", "Customer Service");
                });
            }

            app.MapControllers();
            app.Run();
        }   
    }
}