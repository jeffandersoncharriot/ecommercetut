using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Db
{
    public class  OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}