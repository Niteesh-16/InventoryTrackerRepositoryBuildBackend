using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Application.DTOs
{
    public class InventoryDto
    {

        public int  Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Threshold { get; set; }
    }
}
