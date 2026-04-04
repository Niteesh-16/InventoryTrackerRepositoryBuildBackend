
using InventoryTracker.Domain.Entities;
namespace InventoryTracker.Application.Interfaces
{
    public interface IInventoryRepository
    {

        Task AddAsync(Inventory inventory);

        Task<List<Inventory>> GetAllAsync();

        Task<Inventory?> GetByIdAsync(int id);

        Task UpdateAsync(Inventory inventory);

        Task SaveAsync();

        Task DeleteAsync(Inventory item);

       
        Task<List<Inventory>> GetLowStockAsync();
    }
}
