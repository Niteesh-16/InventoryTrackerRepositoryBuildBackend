using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Text;

namespace InventoryTracker.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int InventoryId { get; set; }

        public string Type { get; set; } 

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public Inventory Inventory { get; set; }
    }
}
