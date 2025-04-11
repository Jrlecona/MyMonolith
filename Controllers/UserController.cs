using Microsoft.AspNetCore.Mvc;
using MyMonolith.Data;
using MyMonolith.Models;

namespace MyMonolith.Controllers;

[ApiController]
[Route("[api/users]")]
public class UserController : Controller
{
    private readonly StoreContext _context;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, StoreContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View("Error!");
    // }
}
