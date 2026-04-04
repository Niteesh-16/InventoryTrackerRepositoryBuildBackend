using System;
using System.Collections.Generic;
using System.Text;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Application.DTOs;
using InventoryTracker.Application.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace InventoryTracker.Application.Services
{
    public class InventoryService: IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IAlertRepository _alertRepo;

        public InventoryService(
            IInventoryRepository inventoryRepo,
            ITransactionRepository transactionRepo,
            IAlertRepository alertRepo
            )
        {
            _inventoryRepo = inventoryRepo;
            _transactionRepo = transactionRepo;
            _alertRepo = alertRepo;
        }

        public async Task CreateAsync(CreateInventoryDto dto)
        {
            var item = new Inventory
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                Threshold = dto.Threshold

            };



            await _inventoryRepo.AddAsync(item);
            await _inventoryRepo.SaveAsync();

            if (item.Quantity < item.Threshold)
            {
                var alert = new Alert
                {
                    InventoryId = item.Id,
                    Message = $"{item.Name} is low on stock. Quantity: {item.Quantity}",
                    IsActive = true
                };

                await _alertRepo.AddAsync(alert);
                await _alertRepo.SaveAsync();
            }


        }

            public async Task<List<InventoryDto>> GetAllAsync()
        {
                var data = await _inventoryRepo.GetAllAsync();
            

                return data.Select(x => new InventoryDto
                {
                    Id = x.Id,
                    Name =x.Name,
                    Quantity = x.Quantity,
                    Threshold = x.Threshold
                }).ToList();
        
        }

        public async Task AddStockAsync(StockDto dto) 
        {
            var item = await _inventoryRepo.GetByIdAsync(dto.InventoryId);
             if (item == null)
                throw new Exception("Item not found");

            item.Quantity += dto.Quantity;

            await _inventoryRepo.UpdateAsync(item);

            if (item.Quantity >= item.Threshold)
            {
                var alert = await _alertRepo
                    .GetActiveAlertByInventoryIdAsync(item.Id);

                if (alert != null)
                {
                    alert.IsActive = false;
                }
            }

             if (item.Quantity < item.Threshold)
            {
                var existingAlert = await _alertRepo
                    .GetActiveAlertByInventoryIdAsync(item.Id);

                if (existingAlert == null)
                {
                    var alert = new Alert
                    {
                        InventoryId = item.Id,
                        Message = $"{item.Name} is low on stock. Quantity: {item.Quantity}",
                        IsActive = true
                    };

                    await _alertRepo.AddAsync(alert);
                }
            }
            var transaction = new Transaction
            {
                InventoryId = item.Id,
                Quantity = dto.Quantity,
                Type = "ADD",
                CreatedAt = DateTime.Now
            };

            await _transactionRepo.AddAsync(transaction);

            await _inventoryRepo.SaveAsync();
            await _alertRepo.SaveAsync();
        }

        public async Task RemoveStockAsync(StockDto dto)
        {
            var item = await _inventoryRepo.GetByIdAsync(dto.InventoryId);

            if (item == null)
                throw new Exception("Item not found");

            if (item.Quantity < dto.Quantity)
                throw new Exception("Not enough stock");

            item.Quantity -= dto.Quantity;

            await _inventoryRepo.UpdateAsync(item);

          
            if (item.Quantity < item.Threshold)
            {
                var existingAlert = await _alertRepo
                    .GetActiveAlertByInventoryIdAsync(item.Id);

                if (existingAlert == null)
                {
                    var alert = new Alert
                    {
                        InventoryId = item.Id,
                        Message = $"{item.Name} is low on stock. Quantity: {item.Quantity}",
                        IsActive = true
                    };

                    await _alertRepo.AddAsync(alert);
                }
            }

            if (item.Quantity >= item.Threshold)
            {
                var alert = await _alertRepo
                    .GetActiveAlertByInventoryIdAsync(item.Id);

                if (alert != null)
                {
                    alert.IsActive = false;
                }
            }
            

            var transaction = new Transaction
            {
                InventoryId = item.Id,
                Quantity = dto.Quantity,
                Type = "REMOVE",
                CreatedAt = DateTime.Now
            };

            await _transactionRepo.AddAsync(transaction);

            await _inventoryRepo.SaveAsync();
            await _alertRepo.SaveAsync();
        }


        public async Task<InventoryDto> GetByIdAsync(int id)
        {
            var item = await _inventoryRepo.GetByIdAsync(id);

            if(item == null)
            {
                throw new Exception("Item not found");

            }
            return new InventoryDto
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity
            };

        }

        public async Task UpdateAsync(UpdateInventoryDto dto)
        {
            var item = await _inventoryRepo.GetByIdAsync(dto.Id);

            if (item == null)
                throw new Exception("Item not found");

            item.Name = dto.Name;

            await _inventoryRepo.UpdateAsync(item);
            await _inventoryRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _inventoryRepo.GetByIdAsync(id);

            if (item == null)
                throw new Exception("Item not found");

         
            await _inventoryRepo.DeleteAsync(item);

          
            await _inventoryRepo.SaveAsync();
        }

        public async Task<InventoryReportDto> GetReportAsync()
        {
            var items = await _inventoryRepo.GetAllAsync();
            var lowStockItems = await _inventoryRepo.GetLowStockAsync();

            var lowStockDto = lowStockItems.Select(x => new LowStockItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity
            }).ToList();

            return new InventoryReportDto
            {
                TotalItems = items.Count,
                TotalQuantity = items.Sum(x => x.Quantity),
                LowStockCount = lowStockDto.Count,
                LowStockItems = lowStockDto

            };

        }
        public async Task<List<LowStockItemDto>> GetLowStockAsync()
        {
            var lowStockItems = await _inventoryRepo.GetLowStockAsync();

            return lowStockItems.Select(x => new LowStockItemDto
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity

            }).ToList();

        }
    }
}
