
using System.ComponentModel.DataAnnotations;
namespace InventoryTracker.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public String Username { get; set; }

        public String Password { get; set; }

        public String Role {  get; set; }

    }
}
