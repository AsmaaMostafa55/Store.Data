
using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Repositry;
using Store.Repositry.Interfaces;
using Store.Repositry.Repositries;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;
using Store.Web.Helper;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductService,ProductService>();

            builder.Services.AddAutoMapper(typeof(ProductProfile));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();
            await ApplySeeding.ApplySeedingAsync(app);
           
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
