using Microsoft.EntityFrameworkCore;
using MyMonolith.Data;
using MyMonolith.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options => options.UseInMemoryDatabase("StoreDB")); // database in memory

var app = builder.Build();

// Config middleware
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

//Initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();

    //add products
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product
            {
                Name = "Laptop",
                Price = 999.99m,
                Stock = 10
            },
            new Product
            {
                Name = "Mouse",
                Price = 19.99m,
                Stock = 50
            }
        );

        //add users
        context.Users.AddRange(
            new User
            {
                UserName = "jorge_fragoso",
                Email = "jfragoso@example.com",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                UserName = "valente_mora",
                Email = "vmora@example.com",
                CreatedAt = DateTime.UtcNow
            }
        );

        context.SaveChanges();

        //add order
        var user = context.Users.First();
        var product = context.Products.First();

        context.Orders.Add(
            new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = 1,
                        Quantity = 1,
                        UnitPrice = 999.99m,
                        Product = product
                    }
                },
                Total = 999.99m
            }
        );

        context.SaveChanges();
    }
}

app.Run();
