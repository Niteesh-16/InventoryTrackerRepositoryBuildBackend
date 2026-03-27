using System;
using System.Collections.Generic;
using System.Text;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Application.DTOs;
using InventoryTracker.Application.Interfaces;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace InventoryTracker.Application.Services
{
    public class InventoryService: IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepo;
        private readonly ITransactionRepository _transactionRepo;

        public InventoryService(
            IInventoryRepository inventoryRepo,
            ITransactionRepository transactionRepo
            )
        {
            _inventoryRepo = inventoryRepo;
            _transactionRepo = transactionRepo;

        }

        public async Task CreateAsync(CreateInventoryDto dto)
        {
            var entity = new Inventory
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                Threshold = dto.Threshold

            };

            await _inventoryRepo.AddAsync(entity);
            await _inventoryRepo.SaveAsync();


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

        public async Task AddStockAsync (StockDto dto)
        {
            var item = await _inventoryRepo.GetByIdAsync(dto.InventoryId);


            if (item == null)
            {
                throw new Exception("Item not found");
            }

            item.Quantity += dto.Quantity;
            
            await  _inventoryRepo.UpdateAsync(item);


            await _transactionRepo.AddAsync(new Transaction
            {
                InventoryId = dto.InventoryId,
                Quantity = dto.Quantity,
                Type = "IN"
            });

            await _inventoryRepo.SaveAsync();

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

            await _transactionRepo.AddAsync(new Transaction
            {
                InventoryId = dto.InventoryId,
                Quantity = dto.Quantity,
                Type = "OUT"
            });

            await _inventoryRepo.SaveAsync();
        }

    }
}
