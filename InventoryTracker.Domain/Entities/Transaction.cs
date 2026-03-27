using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int InventoryId { get; set; }

        public string Type { get; set; } // IN / OUT

        public int Quantity { get; set; }

        // Navigation property
        public Inventory Inventory { get; set; }
    }
}
