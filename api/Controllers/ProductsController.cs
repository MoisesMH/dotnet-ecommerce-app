// Used when the StoreContext class was defined here
// using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// in global usings
using System.Threading.Tasks;
using System.Linq;
using Core.Interfaces;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // private readonly StoreContext _context;
    // public ProductsController(StoreContext context)
    // {
    //     _context = context;
    // }

    // Now we've implemented the StoreContext methods in the ProductRepository class
    // private readonly IProductRepository _repo;
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    
    // public ProductsController(IProductRepository repo)
    public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
    {
        // _repo = repo;
        _productsRepo = productsRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
    }

    // http response status, give us a ResultSet with a list of Product Entities
    // make this function asynchronous, then establish it as a task
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        // Give us a set of products belonging to the database
        // var products = _context.Products.ToList();
        // The same thing as before but it runs asynchronously
        // var products = await _context.Products.ToListAsync();
        // var products = await _repo.GetProductsAsync();
        var products = await _productsRepo.GetAllAsync();
        // return "This is a list of Products";
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        // return "This is a product";
        // var products = await _context.Products.FindAsync(id);
        var products = await _productsRepo.GetByIdAsync(id);
        return Ok(products);
    }
    
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        // Give us a set of products belonging to the database
        // var products = _context.Products.ToList();
        // The same thing as before but it runs asynchronously
        // var products = await _context.Products.ToListAsync();
        var products = await _productBrandRepo.GetAllAsync();
        // return "This is a list of Products";
        return Ok(products);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
    {
        // Give us a set of products belonging to the database
        // var products = _context.Products.ToList();
        // The same thing as before but it runs asynchronously
        // var products = await _context.Products.ToListAsync();
        // var products = await _repo.GetProductTypesAsync();
        var products = await _productTypeRepo.GetAllAsync();
        // return "This is a list of Products";
        return Ok(products);
    }
}