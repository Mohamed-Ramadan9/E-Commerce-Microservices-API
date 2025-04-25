using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.SharedLibrary.Dependency_Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderAPi_Application.Interfaces;
using OrderAPi_Infrastructure.Data;
using OrderAPi_Infrastructure.Repositories;

namespace OrderAPi_Infrastructure.Dependency_Injection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastrucutreService(this IServiceCollection services , IConfiguration configuration)
        {
            // Add DataBase Connectivity
            //Add authentication scheme
            SharedServicesContainer.AddSharedServices<OrderDbContext>(services, configuration  , "OrderAPi", configuration["MySerilog:FileName"]!);

            services.AddScoped<IOrder, OrderRepository>();
            return services;
        }

        public static IApplicationBuilder UserInfrastructurePolicy(this IApplicationBuilder app)
        {
            // Register middleware such as :
            // 1- Global Exception which handle external errors
            // 2- ListenToApiGateway which block all outsiders calls
            SharedServicesContainer.UseSharedPolices(app);
            return app;
        }
    }
}
