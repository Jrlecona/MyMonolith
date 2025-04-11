using Microsoft.AspNetCore.Mvc;
using MyMonolith.Data;
using MyMonolith.Models;

namespace MyMonolith.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : Controller
{
    private readonly StoreContext _context;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger, StoreContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }

    [HttpPost]
    public IActionResult AddProduct([FromBody] Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View("Error!");
    // }
}
