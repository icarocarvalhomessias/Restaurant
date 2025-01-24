using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Controllers.Inputs;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct(ProductInput productInput)
    {
        var product = new Product(
            productInput.Name,
            productInput.Description,
            productInput.Price,
            productInput.Image,
            productInput.Stock
        );

        var result = await _productService.AddProductAsync(product);
        if (!result)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(Guid id, ProductInput productInput)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        product.UpdateDetails(
            productInput.Name,
            productInput.Description,
            productInput.Price,
            productInput.Image,
            productInput.Stock
        );

        var result = await _productService.UpdateProductAsync(product);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var result = await _productService.DeleteProductAsync(product);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
