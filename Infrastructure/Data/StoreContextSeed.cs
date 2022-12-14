using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        var execAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        // var execAssemblyPath = AppDomain.CurrentDomain.BaseDirectory;
        var dataPath = Path.Combine(execAssemblyPath, "../../../../Infrastructure/Data");
        var seedDataPath = "./SeedData/";
        var path = Path.Combine(dataPath, seedDataPath);
        try {
            // For all products
            if (!context.ProductBrands.Any())
            {
                using FileStream brandsData = File.OpenRead(Path.Combine(path, "brands.json"));
                var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandsData);
                
                foreach (var item in brands)
                {
                    context.ProductBrands.Add(item);
                }

                await context.SaveChangesAsync();
            }
            // For types
            if (!context.ProductTypes.Any())
            {
                using FileStream typesData = File.OpenRead(Path.Combine(path, "types.json"));
                var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(typesData);
                
                foreach (var item in types)
                {
                    context.ProductTypes.Add(item);
                }

                await context.SaveChangesAsync();
            }
            // For all products
            if (!context.Products.Any())
            {
                using FileStream productsData = File.OpenRead(Path.Combine(path, "products.json"));
                var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                
                foreach (var item in products)
                {
                    context.Products.Add(item);
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex) {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}
