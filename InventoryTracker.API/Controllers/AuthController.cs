using InventoryTracker.Application.DTOs;
using InventoryTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _service.RegisterAsync (dto);
            return Ok("User Registered");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.LoginAsync (dto);
            return Ok(token);

        }
            
    }
}
