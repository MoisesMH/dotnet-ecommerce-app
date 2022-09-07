using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    // We're just reading the returned result list, not editing it
    // So we can use a read-only list in this case
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
}

// Now that we have our IProductRepository, we create our particular class
// which is going to implement this interface in Infrastructure/Data