using InventoryTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Application.Interfaces
{
    public interface IInventoryService
    {
        Task CreateAsync(CreateInventoryDto dto);

        Task<List<InventoryDto>> GetAllAsync();

        Task AddStockAsync(StockDto dto);

        Task RemoveStockAsync(StockDto dto);
    }
}
