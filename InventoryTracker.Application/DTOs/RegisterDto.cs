using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace InventoryTracker.Application.DTOs
{
    public class RegisterDto
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public string AdminCode { get; set; }


    }
}
