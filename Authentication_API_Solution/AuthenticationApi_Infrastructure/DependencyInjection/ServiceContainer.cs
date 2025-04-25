using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi_Application.Interfaces;
using AuthenticationApi_Infrastructure.Data;
using AuthenticationApi_Infrastructure.Repositories;
using E_Commerce.SharedLibrary.Dependency_Injection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationApi_Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services , IConfiguration configuration)
        {
            // Add database connectivity
            //JWT add Authentication scheme
            SharedServicesContainer.AddSharedServices<AuthenticationDbContext>(services, configuration , "AuthAPi" , configuration["MySerilog:FileName"]);

            services.AddScoped<IUser, UserRespository>();
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IApplicationBuilder UserInfrastructurePolicy(this IApplicationBuilder app)
        {
            // Register middleware such as :
            //Global Exception : Handle external errors.
            //Listen Only to Api Gateway : block all outsiders call.

            SharedServicesContainer.UseSharedPolices(app);
            return app;
        }
    }
}
