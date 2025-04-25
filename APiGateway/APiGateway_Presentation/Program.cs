using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using E_Commerce.SharedLibrary.Dependency_Injection;
using APiGateway_Presentation.Middleware;
using Ocelot.Middleware;
namespace APiGateway_Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddJsonFile("ocelot.json" , optional: false , reloadOnChange:true);
            builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
            JWTAuthenticationScheme.AddJWTAuthenticationScheme(builder.Services, builder.Configuration);

            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });

            });
            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMiddleware<AttatchedSignatureRequest>();
            app.UseOcelot().Wait();
            app.Run();
        }
    }
}
