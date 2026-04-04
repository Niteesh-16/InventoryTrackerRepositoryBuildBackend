using InventoryTracker.Application.DTOs;

namespace InventoryTracker.Application.Interfaces
{
    public interface IInventoryService
    {
        Task CreateAsync(CreateInventoryDto dto);

        Task<List<InventoryDto>> GetAllAsync();

        Task AddStockAsync(StockDto dto);


        Task RemoveStockAsync(StockDto dto);

        Task<InventoryDto> GetByIdAsync(int id);
        Task UpdateAsync(UpdateInventoryDto dto);
        Task DeleteAsync(int id);

        Task<InventoryReportDto> GetReportAsync();
        Task<List<LowStockItemDto>> GetLowStockAsync();
    }
}
