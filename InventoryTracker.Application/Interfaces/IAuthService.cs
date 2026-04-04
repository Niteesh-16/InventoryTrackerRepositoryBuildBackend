
using InventoryTracker.Application.DTOs;

namespace InventoryTracker.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
