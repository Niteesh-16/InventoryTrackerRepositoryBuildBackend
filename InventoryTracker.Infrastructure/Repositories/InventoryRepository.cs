using InventoryTracker.Domain.Entities;
using InventoryTracker.Application.Interfaces;
using InventoryTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository

    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context) 
        {
            _context = context;

        }

        public async Task AddAsync(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory); 
        }

        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Inventory item)
        {
            _context.Inventories.Remove(item);
            await Task.CompletedTask;
        }


        public async Task<List<Inventory>> GetLowStockAsync()
        {
            return await _context.Inventories
                .Where(x => x.Quantity < x.Threshold)
                .ToListAsync();
        }
    }
}
