using Microsoft.EntityFrameworkCore;

namespace SynapseHealthOrderMonitorAPI.Models
{
    // set up Order DBContext
    public class OrderMonitorContext : DbContext
    {
        public OrderMonitorContext(DbContextOptions<OrderMonitorContext> options)
            : base(options) 
        {
            
        }

        public DbSet<Order> Orders => Set <Order>();
    }
}
