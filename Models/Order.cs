using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMonolith.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public required List<OrderItem> Items { get; set; }
    }
}
