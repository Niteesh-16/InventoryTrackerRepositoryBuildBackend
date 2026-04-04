using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        public String Role {  get; set; }

    }
}
