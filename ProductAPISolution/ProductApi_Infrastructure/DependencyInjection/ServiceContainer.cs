using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.SharedLibrary.Dependency_Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi_Infrastructure.Data;
using ProductApi_Infrastructure.Repositories;
using ProductApiApplication.Interfaces;

namespace ProductApi_Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services , IConfiguration config)
        {
            // Add database connectivity 


            // Add authentication scheme
            SharedServicesContainer.AddSharedServices<ProductDbContext>(services, config, "ProductAPi", config["MySerilog:FileName"]);

            // Create Dependency Injection (DI)
            services.AddScoped<IProduct , ProductRepository>();

            return services;


        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)

        {
            // Register middleware such as :
            //Global Exception handles external errors.
            //Listen to Only Api Gateway : blocks all outsider calls
            SharedServicesContainer.UseSharedPolices(app);
            return app;
        }
    }
}
