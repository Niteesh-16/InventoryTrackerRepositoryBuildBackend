
using InventoryTracker.Domain.Entities;

namespace InventoryTracker.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);

        Task SaveAsync();

    }
}
