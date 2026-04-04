using InventoryTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Application.Interfaces
{
    public interface IUserRepository
    {

        Task AddAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
        Task SaveAsync();

    }
}
