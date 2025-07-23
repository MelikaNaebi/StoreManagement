using System.Collections.Generic;

namespace APIStoreManagement.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ClothingId { get; set; }
     
        public int Quantity { get; set; }
        public Clothing Clothing { get; set; }
       
        
    }
}
