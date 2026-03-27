using System;
using System.Collections.Generic;
using System.Text;
using InventoryTracker.Application.Interfaces;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Infrastructure.Data;
namespace InventoryTracker.Infrastructure.Repositories
{
    public class TransactionRepository: ITransactionRepository
    {

        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
