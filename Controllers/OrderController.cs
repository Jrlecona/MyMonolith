using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMonolith.Data;
using MyMonolith.Models;

namespace MyMonolith.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly StoreContext _context;

    public OrderController(StoreContext context) => _context = context;

    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = _context.Orders
            .Include(o => o.User)
            .Include(o => o.Items)
            .ThenInclude(oi => oi.Product)
            .ToList();

        return Ok(orders);
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        order.OrderDate = DateTime.UtcNow;
        order.Total = order.Items.Sum(i => i.Quantity * i.UnitPrice);
        _context.Orders.Add(order);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
    }
}
