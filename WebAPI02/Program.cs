using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAPI02.Data;
using WebAPI02.Helpers;
using WebAPI02.Repos;
using WebAPI02.Repos.Interfaces;
using WebAPI02.UnitofWork;

namespace WebAPI02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the DI container.
            builder.Services.AddControllers();

            builder.Services.AddOpenApi();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfile)); 
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(option => {
                    option.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }

            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
