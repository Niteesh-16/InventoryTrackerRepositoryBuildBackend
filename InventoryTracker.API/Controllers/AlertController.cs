using InventoryTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepo;

        public AlertController(IAlertRepository alertRepo)
        {
            _alertRepo = alertRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlerts()
        {
            var alerts = await _alertRepo.GetAllAsync();
            return Ok(alerts);
        }
    }
}
