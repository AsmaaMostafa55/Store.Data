
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Store.Data.Contexts;
using Store.Repositry;
using Store.Repositry.Interfaces;
using Store.Repositry.Repositries;
using Store.Service.HandleResponses;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;
using Store.Web.Extentions;
using Store.Web.Helper;
using Store.Web.Middlewares;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });



            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            { options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")); });


            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var configurations = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(configurations);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddSwagerDocumentation();
            builder.Services.AddCors(Options=>
            {
                Options.AddPolicy("CorsPolicy",Policy=>
                {
                    Policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http:localhost:4200", "http://localhost:4200","http://localhost:26296");
                });

            });
            var app = builder.Build();
            await ApplySeeding.ApplySeedingAsync(app);
           
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
            app.UseMiddleware<ExceptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseAuthorization();
           
            app.MapControllers();

            app.Run();
        }
    }
}
