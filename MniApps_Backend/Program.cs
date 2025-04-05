using TgMiniAppAuth;
using MiniApps_Backend.DataBase.Extension;
using MiniApps_Backend.Business.Extension;

namespace MiniApps_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDataBase(configuration);
            builder.Services.AddBussiness();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTgMiniAppAuth(configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();



            app.Run();
        }
    }
}
