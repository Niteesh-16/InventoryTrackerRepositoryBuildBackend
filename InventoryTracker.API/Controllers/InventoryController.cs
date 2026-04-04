using InventoryTracker.Application.Interfaces;
using InventoryTracker.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("Inventory created successfully");
        }


        [Authorize(Roles = "Admin,Staff")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

       
        [Authorize(Roles = "Admin,Staff")]
        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock(StockDto dto)
        {
            await _service.AddStockAsync(dto);
            return Ok("Stock added successfully");
        }

       
        [Authorize(Roles = "Admin,Staff")]
        [HttpPost("remove-stock")]
        public async Task<IActionResult> RemoveStock(StockDto dto)
        {
            await _service.RemoveStockAsync(dto);
            return Ok("Stock removed successfully");
        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInventoryDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted successfully");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("report")]
        public async Task<IActionResult> GetReport()
        {
            var report = await _service.GetReportAsync();
            return Ok(report);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var data = await _service.GetLowStockAsync();
            return Ok(data);
        }
    }



}

