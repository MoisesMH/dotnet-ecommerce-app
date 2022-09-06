using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// in global usings
using System.Threading.Tasks;
using System.Linq;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;
    public ProductsController(StoreContext context)
    {
        _context = context;
    }

    // http response status, give us a ResultSet with a list of Product Entities
    // make this function asynchronous, then establish it as a task
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        // Give us a set of products belonging to the database
        // var products = _context.Products.ToList();
        // The same thing as before but it runs asynchronously
        var products = await _context.Products.ToListAsync();
        // return "This is a list of Products";
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        // return "This is a product";
        var products = await _context.Products.FindAsync(id);
        return Ok(products);
    }
}