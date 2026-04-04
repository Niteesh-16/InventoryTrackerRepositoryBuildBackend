using InventoryTracker.Domain.Entities;


namespace InventoryTracker.Application.Interfaces
{
    public  interface IAlertRepository
    {
        Task AddAsync(Alert alert);
        Task<Alert> GetActiveAlertByInventoryIdAsync(int inventoryId);
        Task<List<Alert>> GetAllAsync();
        Task SaveAsync();
    }
}
