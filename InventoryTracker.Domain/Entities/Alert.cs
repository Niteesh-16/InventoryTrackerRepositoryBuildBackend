

namespace InventoryTracker.Domain.Entities
{

        public class Alert
        {
            public int Id { get; set; }
            public int InventoryId { get; set; }
            public string Message { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

            public bool IsActive { get; set; } = true;

            

            
        }
    }

