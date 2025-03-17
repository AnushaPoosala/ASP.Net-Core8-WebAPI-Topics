
using AutoMapperUsageInWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AutoMapperUsageInWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options=>
            {
                //Configuring Json Serializer options to keep the oroginal names in serialized and deserialized JSON

                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }
            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
             //Register DBcontext and connection string
            builder.Services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceConfig"));
            }
            );

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
