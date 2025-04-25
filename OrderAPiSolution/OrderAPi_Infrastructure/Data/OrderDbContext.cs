using Microsoft.EntityFrameworkCore;
using OrderAPi_Domain.Entites;

namespace OrderAPi_Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
