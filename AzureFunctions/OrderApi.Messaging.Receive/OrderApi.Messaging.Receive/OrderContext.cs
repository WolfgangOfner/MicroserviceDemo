using Microsoft.EntityFrameworkCore;

namespace OrderApi.Messaging.Receive
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
    }
}