using OrderAPi_Infrastructure.Dependency_Injection;
using OrderAPi_Application.Configurations;
using OrderAPi_Application.DependencyInjection;
namespace OrderAPi_Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           

            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastrucutreService(builder.Configuration);
            builder.Services.AddApplicationService(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(MapperConfig));


            var app = builder.Build();

           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UserInfrastructurePolicy();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
