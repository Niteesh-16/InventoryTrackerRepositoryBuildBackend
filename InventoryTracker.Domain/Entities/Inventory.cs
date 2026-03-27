using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace InventoryTracker.Domain.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public int Threshold { get; set; }

        public ICollection<Transaction> Transactions { get; set; } // this is basically telling that we are having a relation to 
        //many transaction ( one to many)
    }
}
