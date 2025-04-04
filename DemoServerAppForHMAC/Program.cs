
using DemoServerAppForHMAC.Context;
using DemoServerAppForHMAC.Middleware;
using Microsoft.EntityFrameworkCore;

namespace DemoServerAppForHMAC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Adding In-Memory Cache
            builder.Services.AddMemoryCache();
           // Adding the ClientService
            builder.Services.AddScoped<Models.Services.ClientService>();


            builder.Services.AddDbContext<ServerAppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ServerDBConfig"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //Register the HMAC Middleware
            app.UseMiddleware<HMACAuthMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
