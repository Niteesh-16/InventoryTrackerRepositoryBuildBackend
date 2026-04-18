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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(); //this makes sure that i put unique index username and making it unique    
        }
    }
}
