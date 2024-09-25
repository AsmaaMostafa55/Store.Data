﻿using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repositry
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if(context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    //presist To database
                    var brandsData = File.ReadAllText("../Store.Repository/SeedData-20240924T205710Z-001/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if(brands is not null)
                    {
                        await context.ProductBrands.AddRangeAsync(brands);
                    }
                }
                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    //presist To database
                    var typesData = File.ReadAllText("../Store.Repository/SeedData-20240924T205710Z-001/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null)
                    {
                        await context.ProductTypes.AddRangeAsync(types);
                    }
                }


                if (context.Products != null && !context.Products.Any())
                {
                    //presist To database
                    var productsData = File.ReadAllText("../Store.Repository/SeedData-20240924T205710Z-001/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null)
                    {
                        await context.Products.AddRangeAsync(products);
                    }
                }
                await context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }

        
        }
    }
}
