using InventoryTracker.Application.Interfaces;
using InventoryTracker.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;



namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController: ControllerBase 
    {

       private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryDto dto)
        {
            await _service.CreateAsync  (dto);
            return Ok("Inventory created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);

        }

        [HttpPost("add-stock")]

        public async Task<IActionResult> AddStock(StockDto dto)
        {
            await _service.AddStockAsync (dto);
            return Ok("Stock added Sucessfully");

        }


        [HttpPost("remove-stock")]
        public async Task<IActionResult> RemoveStock(StockDto dto)
        {
            await _service.RemoveStockAsync(dto);
            return Ok("Stock removed successfully");
        }
    }
}
