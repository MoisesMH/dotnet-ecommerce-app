using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{


    // Passing the particular type
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    // Query the products
    // Also, this will help save new products to the database
    public DbSet<Product> Products { get; set; }
    // Adding new migrations for ProductBrand and ProductType entities
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    // Overriding a method from DbContext
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}