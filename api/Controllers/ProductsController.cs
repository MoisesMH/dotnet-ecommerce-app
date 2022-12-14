// Used when the StoreContext class was defined here
// using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// in global usings
using System.Threading.Tasks;
using System.Linq;
using Core.Interfaces;
using Core.Specifications;
using api.Dtos;
using AutoMapper;
using api.Errors;
using api.Helpers;

namespace api.Controllers;

// [ApiController]
// [Route("api/[controller]")]
public class ProductsController : BaseApiController
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
    private readonly IMapper _mapper;
    // public ProductsController(IProductRepository repo)
    public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
    {
        // _repo = repo;
        _productsRepo = productsRepo;
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
        _mapper = mapper;
    }

    // http response status, give us a ResultSet with a list of Product Entities
    // make this function asynchronous, then establish it as a task
    [HttpGet]
    // public async Task<ActionResult<List<Product>>> GetProducts()
    // public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    // public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    // public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort, int? brandId, int? typeId)
    // public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(
    //     // Important to consider params as a string
    //     [FromQuery] ProductSpecParams productParams
    // )
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
        // Important to consider params as a string
        [FromQuery] ProductSpecParams productParams
    )
    {
        // Give us a set of products belonging to the database
        // var products = _context.Products.ToList();
        // The same thing as before but it runs asynchronously
        // var products = await _context.Products.ToListAsync();
        // var products = await _repo.GetProductsAsync();
        // var products = await _productsRepo.GetAllAsync();
        // Using generic repository
        // Specification method
        // var spec = new ProductsWithTypesAndBrandsSpecification(sort, brandId, typeId);
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var countSpec = new ProductWithFiltersForCountSpecification(productParams);

        var totalItems = await _productsRepo.CountAsync(countSpec);
        var products = await _productsRepo.ListAsync(spec);

        var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
        
        // return "This is a list of Products";
        // return Ok(products);
        // No automapper
        // return products.Select(product => new ProductToReturnDto {
        //     Id = product.Id,
        //     Name = product.Name,
        //     Description = product.Description,
        //     PictureUrl = product.PictureUrl,
        //     Price = product.Price,
        //     ProductBrand = product.ProductBrand.Name,
        //     ProductType = product.ProductType.Name
        // }).ToList<ProductToReturnDto>();
        // With automapper
        return Ok(new Pagination<ProductToReturnDto>(
            productParams.PageIndex,
            productParams.PageSize,
            totalItems,
            data));
    }

    [HttpGet("{id}")]
    // Custom error handling response for controllers
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<Product>> GetProduct(int id)
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        // return "This is a product";
        // var products = await _context.Products.FindAsync(id);
        // var products = await _productsRepo.GetByIdAsync(id);
        // Using generic repository
        // Specification method
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var product = await _productsRepo.GetEntityWithSpec(spec);

        if (product == null) return NotFound(new ApiResponse(404));

        // return Ok(products);
        return _mapper.Map<Product, ProductToReturnDto>(product);
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