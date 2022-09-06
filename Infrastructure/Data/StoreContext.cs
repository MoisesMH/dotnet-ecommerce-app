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
    public DbSet<Product> Products { get; set; }
}