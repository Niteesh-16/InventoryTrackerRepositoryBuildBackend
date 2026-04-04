

namespace InventoryTracker.Application.DTOs
{
    public class InventoryReportDto
    {
       
            public int TotalItems { get; set; }
            public int TotalQuantity { get; set; }
            public int LowStockCount { get; set; }
            public List<LowStockItemDto> LowStockItems { get; set; }
        
    }
}
