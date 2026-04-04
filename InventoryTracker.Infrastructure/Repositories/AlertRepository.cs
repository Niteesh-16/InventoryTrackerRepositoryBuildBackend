using InventoryTracker.Application.Interfaces;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Infrastructure.Repositories
{
  
        public class AlertRepository : IAlertRepository
        {
            private readonly AppDbContext _context;

            public AlertRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(Alert alert)
            {
                await _context.Alerts.AddAsync(alert);
            }

            public async Task<Alert> GetActiveAlertByInventoryIdAsync(int inventoryId)
            {
                return await _context.Alerts
                    .FirstOrDefaultAsync(x => x.InventoryId == inventoryId && x.IsActive);
            }

            public async Task<List<Alert>> GetAllAsync()
            {
                return await _context.Alerts.ToListAsync();
            }

            public async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
    }

