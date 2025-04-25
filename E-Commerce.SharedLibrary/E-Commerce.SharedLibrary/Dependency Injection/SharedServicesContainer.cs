using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace E_Commerce.SharedLibrary.Dependency_Injection
{
    public static class SharedServicesContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services , IConfiguration config , string connectionName , string fileName) where TContext : DbContext
        {
            // Add Generic database context
            var connectionString = config.GetConnectionString(connectionName);

            services.AddDbContext<TContext>(options =>
                 options.UseSqlServer(connectionString, sqlOptions =>
                 {
                     sqlOptions.EnableRetryOnFailure(
                         maxRetryCount: 5,             // Retry up to 5 times
                         maxRetryDelay: TimeSpan.FromSeconds(10), // Wait up to 10s between retries
                         errorNumbersToAdd: null       
                     );
                 }));

            // configure Serilog logging
            Log.Logger = new LoggerConfiguration().MinimumLevel.
                Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path:$"{fileName}-.text" ,
                restrictedToMinimumLevel :Serilog.Events.LogEventLevel.Information,
                outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}" ,
                rollingInterval:RollingInterval.Day).CreateLogger();

            // Add JWT Authentication Scheme
            JWTAuthenticationScheme.AddJWTAuthenticationScheme(services, config);

            return services;
        }

        public static IApplicationBuilder UseSharedPolices( this IApplicationBuilder app)
        {
            // Use global Exception
            app.UseMiddleware<GlobalException>();
            
            // Register Middleware to block outsider API calls
            //app.UseMiddleware<ListenToOnlyApiGateway>();

            return app;

        }
    }
}
