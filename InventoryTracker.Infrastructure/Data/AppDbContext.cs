using Microsoft.EntityFrameworkCore;
using InventoryTracker.Domain.Entities;
namespace InventoryTracker.Infrastructure.Data
{
    public class AppDbContext:DbContext

    {
           public AppDbContext(DbContextOptions<AppDbContext> options):base(options ) 
        { 
                
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        
        public DbSet<User> Users { get; set; }
       
    }
}
